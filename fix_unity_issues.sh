#!/bin/bash 
path=${PWD}
unity_location=/Applications/Unity/Unity.app/Contents/MacOS/Unity
guisystem_location=/Applications/Unity/Unity.app/Contents/UnityExtensions/Unity
echo $path

mkdir -p ~/tmp/
mv $guisystem_location/GUISystem ~/tmp
mv $guisystem_location/Networking ~/tmp
mv $guisystem_location/UnityAnalytics ~/tmp

$unity_location -projectPath $path -quit
mv ~/tmp/GUISystem $guisystem_location
mv ~/tmp/Networking $guisystem_location
mv ~/tmp/UnityAnalytics $guisystem_location

