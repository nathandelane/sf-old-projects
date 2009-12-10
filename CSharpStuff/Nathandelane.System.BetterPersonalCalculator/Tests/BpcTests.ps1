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

Write-Output ""
Write-Output "BPC Tests - PC.NET"
Write-Output "This script comprises a series of tests to be run against BPC - Better Personal Calculator."
Write-Output "This script's version is 1.0 and should be used against builds 1.2.0.0 and higher of BPC."

$anyFailed = $False
$numberFailed = 0
$expressions = "12 + 1 # Simple Addition", "19 - 4 # Simple Subtraction", "9 * 8 # Simple Multiplication", "13 / 2 # Simple Division", "-1 + 2 # Negative Addition", "-(1 + 2) # Negative Distribution", "-(-53 + -17) # More Complex Negative Distribution", "21 / 3 # Simple Division", "9 // 8 # Simple DivRem", "19 // 8 # Simple DivRem", "21 * 1.5 # Decimal Multiplication", "2 ** 3 # Simple Power", "15 ** 2 # Simple Power", "-6 / 2 # Negative Division", "-15 / 2 # Negative Division", "-15 // 2 # Negative DivRem", "-15 % 2 # Negative Modulus", "100 / 19 # Simple Division", "100 // 19 # Simple DivRem", "100 % 19 # Simple Modulus", "(-21 - 1) / 2 # Perentheses", "e ** 2 # E and Power", "4 ** 2 # Simple Power", "1.5 ** 3 # Fractional Power", "--mode-degrees tan(45) # Mode=degrees and Trig", "--mode-degrees cos(35) # Mode=degrees and Trig", "--mode-degrees atan(1) # Mode=degrees and Trig", "pi # PI", "e # E", "4 ** (1 / 2) # Fractional Power", "sqrt(4) # Square Root", "8 ** (1 / 3) # Fractional Power", "4! # Simple Factorial", "16! - 13! # Factorial Subtraction", "((19 + 42 - 12) * 3) ** 2 # Complex Perentheses", "(19 + 42 - 12 * 3) ** 2 # Complex Order of Operations", "tod(ch) # To Decimal on Hexadecimal", "toh(18) # To Hex on Decimal", "tod(29h + fh) # To Decimal on Hexadecimal Addition", "fh + 1111b / 2 # Multiple bases, last base takes hold"
$expected = 13, 15, 72, 6.5, 1, -3, 70, 7, 1, 2, 31.5, 8, 225, -3, -7.5, -7, -1, 5.26315789473684, 5, 5, -11, 7.38905609893068, 16, 3.375, 1, 0.819152044288992, 45, [System.Math]::PI, [System.Math]::E, 2, 2, 2, 24, 20916562867200, 21609, 625, 12, "12h", 56, 15

function AssertEquals([string]$expected, [string]$actual)
{
	$ex = $expected
	$ac = $actual

	if(!$expected.EndsWith("h") -and !$actual.EndsWith("h"))
	{
		$ex = [long]$expected
		$ac = [long]$actual
	}
	
	if($ac -eq $ex)
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
	Write-Output "bpc <expression> = expected (actual)...Pass or Fail"
	Write-Output ""

	$numberOfTests = $expressions.Length
	$expressionIndex = 0

	do
	{
		$expr = $expressions[$expressionIndex]
		$bpcResult = (bpc $expr.Split(" "))
		$expct = $expected[$expressionIndex]
		$result = AssertEquals $expct $bpcResult
		Write-Output "bpc $expr = $expct ($bpcResult)...$result"
		
		if($result -eq "Failed")
		{
			$anyFailed = $True
			$numberFailed++
		}
		
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
	Write-Output ""
	Write-Output "An error occurred: the number of expected results do not match the number of expressions available."
	Write-Output ""
}
