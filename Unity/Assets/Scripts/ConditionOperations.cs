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
		else
		{
			//treat value as a number, compare with parameter by selected compare function
			switch (value.Operator)
			{
				case CompareFunction.Equals:
					return value.Value == parameter.Value;
				case CompareFunction.Greater:
					return value.Value > parameter.Value;
				case CompareFunction.Smaller:
					return value.Value < parameter.Value;
				case CompareFunction.GEqual:
					return value.Value >= parameter.Value;
			}
		}
		return true;
	}
}