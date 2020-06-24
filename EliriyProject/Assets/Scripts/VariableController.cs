using Ink.Runtime;
using UnityEngine;


public class VariableController : MonoBehaviour
{
  [SerializeField] private TextAsset main_variable_asset = null;

  private Story global_variables = null;
  
  public VariableController instance { get; private set; } = null;

  
  private void Awake()
  {
    instance = this;
    
    global_variables = new Story( main_variable_asset.text );

    Story.VariableObserver variable_observer = ( variable_name, value ) => Debug.Log( $"{variable_name} = {value}" );
  }

  public void setVariable( string variable, string value )
  {
    global_variables.variablesState[variable] = value;
  }

  public void setVariable( string variable, int value )
  {
    global_variables.variablesState[variable] = value;
  }

  public string getVariable( string variable )
  {
    return global_variables.variablesState[variable].ToString();
  }

  public bool itsTrue( string variable )
  {
    return getVariable( variable ).Equals( Constant.TRUE );
  }

  public bool itsFalse( string variable )
  {
    return getVariable( variable ).Equals( Constant.FALSE );
  }
}
