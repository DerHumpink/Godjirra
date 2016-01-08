using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogStage
{
	public int Id=-1;
	public string Name="name";
	public string Text="text";
	public List<Answer> Answers=new List<Answer>();
}

[Serializable]
public class Answer
{
	public int NextStageId=0;
	public string Text="text";
	public List<Condition>	Conditions = new List<Condition>();
	public List<Effect>	Effects = new List<Effect>();
}

[Serializable]
public class Condition
{
	public int parameterID=0;
	public int Value=0;
	public CompareFunction Operator;
}

[Serializable]
public class Effect
{
	public int parameterID = 0;
	public int Value = 0;
	public Operator Operator;
}

public enum CompareFunction
{
	Equals,
	Greater,
	Smaller
}

public enum Operator
{
	Set,
	Add,
	Subtract
}