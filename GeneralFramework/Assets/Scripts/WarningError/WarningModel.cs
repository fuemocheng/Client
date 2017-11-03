using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void WarningResult();
public class WarningModel
{
    public WarningResult m_delResult;
    public string m_sValue;

    public WarningModel(string value, WarningResult result = null)
    {
        this.m_sValue = value;
        this.m_delResult = result;
    }
	
}
