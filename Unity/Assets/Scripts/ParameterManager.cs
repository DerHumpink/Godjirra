using System.Collections.Generic;
using System.Linq;

public class ParameterManager : Singleton<ParameterManager>
{
	public List<RuntimeParameter> GameParameters { get; private set; }

	private void Start()
	{
		//Convert all Parameters to RuntimeParameters
		GameParameters = ParameterSetup.Instance.Parameters.Select(parameter => new RuntimeParameter(parameter)).ToList();
	}

	public RuntimeParameter GetParameterFromId(int parameterId)
	{
		return GameParameters.FirstOrDefault(p => p.Id == parameterId);
	}
}

public class RuntimeParameter
{
	public RuntimeParameter(Parameter parameter)
	{
		Type = parameter.Type;
		Name = parameter.Name;
		Id = parameter.Id;
		Value = parameter.StartValue;
	}

	public int Id			{ get; private set; }
	public string Name		{ get; private set; }
	public ParameterType Type { get; private set; }
	public int Value		{ get; private set; }
}