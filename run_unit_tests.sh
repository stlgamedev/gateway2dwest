#!/bin/bash 
path=${PWD}
echo $path
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath "$path" -executeMethod UnityTest.Batch.RunUnitTests -buildTarget StandaloneOSXUniversal -resultFilePath="$path/build/unitTestResults.xml"
