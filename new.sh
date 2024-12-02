day="$(date '+%-d')"
projectDir="Day$day"

if test -d $projectDir; then
  echo "Project '$projectDir' already exists."
else
  mkdir $projectDir
  cd $projectDir
  dotnet new console --framework net8.0
  dotnet add package Xapier14.AdventOfCode
fi
