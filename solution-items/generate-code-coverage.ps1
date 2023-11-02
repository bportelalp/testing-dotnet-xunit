dotnet test ./../ /p:CollectCoverage=true /p:Include=[*]* --% /p:Exclude=[*]*Common* /p:CoverletOutputFormat=\"opencover,cobertura\" --logger trx --results-directory ./../_temp/ 

./../tools/reportgenerator.exe -reports:"../**/coverage.opencover.xml" -targetdir:"./../_temp/Report"