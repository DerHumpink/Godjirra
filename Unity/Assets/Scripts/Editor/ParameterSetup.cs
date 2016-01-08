using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace Editor
{
	public class ParameterSetup : SetupObject<ParameterSetup>
	{
		public ParameterSetup()
		{
			Parameters = new List<Parameter>();
		}

		public List<Parameter> Parameters;

		public void AddParameter()
		{
			Parameter p = new Parameter {Id = GetUniqueId(),Name = UnityEngine.Random.value.ToString()};
			Parameters.Add(p);
		}

		private int GetUniqueId()
		{
			for (int i = 0; i < int.MaxValue; i++)
			{
				if(Parameters.Any(p=>p.Id==i))
					continue;
				return i;
			}
			return -1;
		}

		public void RemoveParameter(Parameter parameter)
		{
			Parameters.Remove(parameter);
		}

		public string GetParameterName(int parameterID)
		{
			Parameter parameter = Parameters.First(p => p.Id == parameterID);
			if (parameter == null)
			{
				return "Attribute Name not found";
			}
			return parameter.Name;
		}

		public string[] GetAllParameterNames()
		{
			return Parameters.Select(p => p.Name).ToArray();
		}

		public int GetParameterIdByIndex(int selection)
		{
			if (selection < Parameters.Count)
			{
				return Parameters[selection].Id;
			}
			else
			{
				return -1;
			}
		}

		public Parameter GetParameterById(int parameterId)
		{
			Parameter parameter=Parameters.First(p => p.Id == parameterId);
			return parameter;
		}
		public int GetParameterIndexById(int parameterId)
		{
			if (Parameters.Any(p => p.Id == parameterId))
			{
				return Parameters.TakeWhile(p => p.Id != parameterId).Count();
			}
			return 0;
		}
	}
	
	[Serializable]
	public class Parameter
	{
		public int Id;
		public string Name;
		public bool Boolean;
		public int Value;
		public ParameterType Type;
	}

	public enum ParameterType
	{
		Number=0,
		Boolean=1
	}
}