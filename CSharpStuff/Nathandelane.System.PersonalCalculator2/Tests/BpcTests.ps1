# Copyright (C) 2009, Nathandelane.
# License:
# Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.
# 
# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 3, or (at your option)
# any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
# 02111-1307, USA.

Write-Output "BPC Tests - PC.NET"
Write-Output "This script comprises a series of tests to be run against BPC - Better Personal Calculator."
Write-Output "This script's version is 1.0 and should be used against builds 1.2.0.0 and higher of BPC."

$anyFailed = $False
$numberFailed = 0
$expressions = "12 + 1", "19 - 4", "9 * 8", "13 / 2", "-1 + 2", "-(1 + 2)", "-(-53 + -17)", "21 / 3", "9 // 8"
$expected = 13, 15, 72, 6.5, 1, -3, 70, 7, 1

function AssertEquals([int]$expected, [int]$actual)
{	
	if($actual -eq $expected)
	{
		return "Passed"
	}
	
	return "Failed"
}

if($expressions.Length -eq $expected.Length)
{
	Write-Output ""

	$bpcVersion = (bpc --version).Split(":")[1].Trim()

	Write-Output "Current version of PC.NET is $bpcVersion"
	Write-Output "Running command-line test suite for PC.NET..."
	Write-Output ""

	$numberOfTests = $expressions.Length
	$expressionIndex = 0

	do
	{
		$expr = $expressions[$expressionIndex]
		$bpcResult = (bpc $expr)
		$expct = $expected[$expressionIndex]
		$result = AssertEquals $expct $bpcResult
		Write-Output "bpc $expr = $expct ($bpcResult)...$result"
		
		$expressionIndex++
	}
	while($expressionIndex -lt $numberOfTests)

	Write-Output ""
	Write-Output "Tests completed."
	Write-Output "$numberOfTests tests were run."
	Write-Output ""

	$numberPassed = $expressions.Length - $numberFailed

	Write-Output "$numberPassed tests passed."
	Write-Output "$numberFailed tests failed."
	Write-Output ""
}
else
{
	Write-Output "An error occurred: the number of expected results do not match the number of expressions available."
}
