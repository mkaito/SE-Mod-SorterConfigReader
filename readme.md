# Sorter Config Reader

This mod adds a button to the terminal controls of sorters to read the filter
configuration from Custom Data.

## Usage

The mod code runs every 100 ticks. Any Sorter with config in Custom Data as
shown below will get configured automatically. The parsing only runs if the
Custom Data has changed, so there is no continued performance cost.

```ini
[SorterConfig]
mode = blacklist
filters =
|Ore/Stone
|Ore/Iron
|Component/SteelPlate
```
