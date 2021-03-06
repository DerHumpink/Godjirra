﻿using System;
using System.Collections.Generic;
using Editor;
using UnityEngine;


public class DialogTreeSetup : SetupObject<DialogTreeSetup>
{
	public List<DialogStage> Stages = new List<DialogStage>();
	public int FistStageId;
}


[Serializable]
public class DialogStage
{
	public List<Answer> Answers = new List<Answer>();
	public int Id = -1;
	public string Name = "";
	public string Text = "";
}

[Serializable]
public class Answer
{
	public List<Condition> Conditions = new List<Condition>();
	public List<Effect> Effects = new List<Effect>();
	public int NextStageId = 0;
	public string Text = "";
}

[Serializable]
public class Condition
{
	public CompareFunction Operator=CompareFunction.GEqual;
	public int ParameterId = 0;
	public int Value = 0;
}

[Serializable]
public class Effect
{
	public Operator Operator=Operator.Add;
	public int ParameterId = 0;
	public int Value = 0;
}

public enum CompareFunction
{
	Equals,
	Greater,
	Smaller,
	GEqual
}

public enum Operator
{
	Set,
	Add,
	Subtract
}