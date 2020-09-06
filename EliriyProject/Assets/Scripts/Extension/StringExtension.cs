using System;


public static class StringExtension
{
  public static string filter( this string text )
  {
    const string SYMBOL_TAB = "<pre>";
    const string TAB        = "     ";

    return text.Replace( SYMBOL_TAB, TAB );
  }

  public static string align( this string text, AlignType align_type = AlignType.CENTER )
  {
    string tag_center = $"<align=\"{getAlignTag( align_type )}\">";
    return $"{tag_center}{text}";
  }

  public static string bold( this string text )
  {
    return $"<b>{text}</b>";
  }

  public static string brackets( this string text )
  {
    return $"[ {text} ]";
  }

  private static string getAlignTag( AlignType align_type )
  {
    switch ( align_type )
    {
      case AlignType.CENTER: return "center";
      case AlignType.LEFT:   return "left";
      case AlignType.RIGHT:  return "right";
      
      default: throw new ArgumentOutOfRangeException( nameof( align_type ), align_type, null );
    }
  }
}

public enum AlignType
{
  CENTER,
  LEFT,
  RIGHT
}
