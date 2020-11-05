using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

IEnumerable<string> oldFileLines = await File.ReadAllLinesAsync("data-old.txt");
oldFileLines = oldFileLines.Skip(1);
var oldLines = oldFileLines.Select(l => l.Split("\t"));

IEnumerable<string> newFileLines = await File.ReadAllLinesAsync("data-new.txt");
newFileLines = newFileLines.Skip(1);
var newLines = newFileLines.Select(l => l.Split("\t"));
bool isNew;
bool contains;

//Checking if a row is considered as deleted
foreach (var oldLine in oldLines)
{
    contains = false;
    foreach (var newLine in newLines)
    {
        if (newLine.Contains(oldLine[0]) && newLine.Contains(oldLine[1]) && newLine.Contains(oldLine[3]))
        {
            contains = true;
            break;
        }
    }
    if(!contains)
    {
        Console.WriteLine($"- {oldLine[0]}\t{oldLine[1]}\t{oldLine[2]}\t{oldLine[3]}\t{oldLine[4]}");
    }
}
Console.WriteLine();
//Checking if a row is considered as changed
foreach (var newLine in newLines)
{
    foreach (var oldLine in oldLines)
    {
        if (oldLine.Contains(newLine[0]) && oldLine.Contains(newLine[1]) && oldLine.Contains(newLine[3]) && (!oldLine.Contains(newLine[2]) || !oldLine.Contains(newLine[4])))
        {
            Console.WriteLine($"≈ {newLine[0]}\t{newLine[1]}\t{newLine[2]}\t{newLine[3]}\t{newLine[4]}");
            break;
        }
    }
}
Console.WriteLine();
//Checking if a row is considered as new
foreach (var newLine in newLines)
{
    isNew = true;  
    foreach (var oldLine in oldLines)
    {
        if (newLine.Contains(oldLine[0]) && newLine.Contains(oldLine[1]) && newLine.Contains(oldLine[3]))
        {
            isNew = false;
        }
    }
    if (isNew) Console.WriteLine($"+ {newLine[0]}\t{newLine[1]}\t{newLine[2]}\t{newLine[3]}\t{newLine[4]}");
}