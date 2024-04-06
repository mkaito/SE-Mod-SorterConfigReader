#!/usr/bin/bash
set -euo pipefail
IFS=$'\n\t'

echo Building...

rm -rf ./Upload
mkdir -p ./Upload/Data/Scripts/SorterConfigReader
cp ./Scripts/SorterConfigReader/SorterConfigReader/*.cs ./Upload/Data/Scripts/SorterConfigReader
rsync --include='*.sbc' --include='*/' --exclude='*' -rt ./Data/ ./Upload/Data/
rsync --include='*.mod' --include='*.sbmi' --include='*.png' --include='*/' --exclude='*' -rt ./Assets/ ./Upload/

echo Done
