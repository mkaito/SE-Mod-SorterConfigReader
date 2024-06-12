using System;
using System.Collections.Generic;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using IMyConveyorSorter = Sandbox.ModAPI.IMyConveyorSorter;

namespace SorterConfigReader
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_ConveyorSorter), false)]
    // ReSharper disable once UnusedType.Global
    public class SorterConfigurator : MyGameLogicComponent
    {
        private readonly MyIni _ini = new MyIni();
        private IMyConveyorSorter _sorter;
        private string _customData = "-797123";

        private const string IniTag = "SorterConfig";
        private const string FilterPrefix = "MyObjectBuilder_";

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            if (MyAPIGateway.Utilities.IsDedicated)
            {
                return;
            }
            
            _sorter = (IMyConveyorSorter)Entity;
            NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            base.UpdateOnceBeforeFrame();

            if (_sorter?.CubeGrid?.Physics == null)
            {
                return;
            }

            NeedsUpdate = MyEntityUpdateEnum.EACH_100TH_FRAME;
        }

        public override void UpdateAfterSimulation100()
        {
            base.UpdateAfterSimulation100();
            if (_customData == _sorter.CustomData)
            {
                return;
            }

            _customData = _sorter.CustomData;

            if (!MyIni.HasSection(_customData, IniTag))
            {
                return;
            }

            MyIniParseResult iniParseResult;
            if (!_ini.TryParse(_customData, out iniParseResult))
            {
                return;
            }

            var modeString = _ini.Get(IniTag, "mode").ToString().Trim();
            var filterString = _ini.Get(IniTag, "filters").ToString().Trim();

            var mode = modeString == "whitelist" ? MyConveyorSorterMode.Whitelist : MyConveyorSorterMode.Blacklist;

            var filters = ParseFilters(filterString);

            _sorter.SetFilter(mode, filters);
        }

        private static List<MyInventoryItemFilter> ParseFilters(string filterString)
        {
            var filters = new List<MyInventoryItemFilter>();
            var lines = filterString.Split('\n');

            foreach (var line in lines)
            {
                var trim = line.Trim();
                try
                {
                    var f = new MyInventoryItemFilter($"{FilterPrefix}{trim}");
                    filters.Add(f);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }

            return filters;
        }
    }
}