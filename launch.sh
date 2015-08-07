#!/bin/bash 
path=${PWD}
echo $path
/Applications/Unity/Unity.app/Contents/MacOS/Unity -projectPath "$path" &
