#!/bin/bash

dotnet publish -c Release -o built -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
sudo cp built/fequ /usr/bin
