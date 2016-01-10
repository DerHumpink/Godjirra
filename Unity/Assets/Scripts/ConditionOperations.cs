using System;

public static class ConditionOperations
{
	public static bool IsFullfielled(this Condition value)
	{
		var parameter = ParameterManager.Instance.GetParameterFromId(value.ParameterId);

		if (parameter.Type == ParameterType.Boolean)
		{
			//treat value as boolean
			return (value.Value > 0) == (parameter.Value > 0);
		}
		//treat value as a number, compare with parameter by selected compare function
		switch (value.Operator)
		{
			case CompareFunction.Equals:
				return parameter.Value == value.Value;
			case CompareFunction.Greater:
				return parameter.Value > value.Value;
			case CompareFunction.Smaller:
				return parameter.Value < value.Value;
			case CompareFunction.GEqual:
				return parameter.Value >= value.Value;
		}
		return true;
	}
}

public static class EffectOperators
{
	public static void Apply(this Effect value)
	{
		var parameter = ParameterManager.Instance.GetParameterFromId(value.ParameterId);
		if (parameter.Type == ParameterType.Boolean)
		{
			parameter.Value = value.Value;
		}
		else
		{
			switch (value.Operator)
			{
				case Operator.Set:
					parameter.Value = value.Value;
					break;
				case Operator.Add:
					parameter.Value += value.Value;
					break;
				case Operator.Subtract:
					parameter.Value += value.Value;
					break;
			}
		}
	}
}