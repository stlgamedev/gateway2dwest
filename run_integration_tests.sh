#!/bin/bash 
path=${PWD}
echo $path
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath "$path" -executeMethod UnityTest.Batch.RunIntegrationTests -testscenes=PlayerMovementTest -buildTarget StandaloneOSXUniversal -resultsFileDirectory="$path/build/integrationTestResults"
