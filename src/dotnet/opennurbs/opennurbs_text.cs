#pragma warning disable 1591
using System;
using System.Runtime.Serialization;
using Rhino.DocObjects;
using Rhino.Runtime;
using Rhino.Runtime.InteropWrappers;

namespace Rhino.Geometry
{
  /// <since>6.0</since>
  public enum TextRunType
  {
    None = UnsafeNativeMethods.TextRunTypeConsts.None,
    Text = UnsafeNativeMethods.TextRunTypeConsts.Text,
    Newline = UnsafeNativeMethods.TextRunTypeConsts.Newline,
    Paragraph = UnsafeNativeMethods.TextRunTypeConsts.Paragraph,
    Column = UnsafeNativeMethods.TextRunTypeConsts.Column,
    Field = UnsafeNativeMethods.TextRunTypeConsts.Field,
    Fontdef = UnsafeNativeMethods.TextRunTypeConsts.Fontdef,
    Header = UnsafeNativeMethods.TextRunTypeConsts.Header
  }

  [Serializable]
  public class TextEntity : AnnotationBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TextEntity"/> class.
    /// </summary>
    /// <example>
    /// <code source='examples\vbnet\ex_textjustify.vb' lang='vbnet'/>
    /// <code source='examples\cs\ex_textjustify.cs' lang='cs'/>
    /// <code source='examples\py\ex_textjustify.py' lang='py'/>
    /// </example>
    /// <since>5.0</since>
    public TextEntity()
    {
      var ptr = UnsafeNativeMethods.ON_V6_TextObject_New();
      ConstructNonConstObject(ptr);
    }

    internal TextEntity(IntPtr nativePointer, object parent)
      : base(nativePointer, parent)
    { }

    /// <summary>
    /// Protected constructor used in serialization
    /// </summary>
    /// <param name="info">Serialization data</param>
    /// <param name="context">Serialization stream</param>
    protected TextEntity(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal override GeometryBase DuplicateShallowHelper()
    {
      return new TextEntity(IntPtr.Zero, null);
    }

    /// <summary> Create Text geometry or null if input is invalid </summary>
    /// <param name="text"></param>
    /// <param name="plane"></param>
    /// <param name="style"></param>
    /// <param name="wrapped"></param>
    /// <param name="rectWidth"></param>
    /// <param name="rotationRadians"></param>
    /// <returns></returns>
    /// <since>6.0</since>
    public static TextEntity Create(string text, Plane plane, DimensionStyle style, bool wrapped, double rectWidth, double rotationRadians)
    {
      return CreateWithRichText(AnnotationBase.PlainTextToRtf(text), plane, style, wrapped, rectWidth,
        rotationRadians);
    }

    /// <summary> Create RichText geometry or null if input is invalid </summary>
    /// <param name="richTextString"></param>
    /// <param name="plane"></param>
    /// <param name="style"></param>
    /// <param name="wrapped"></param>
    /// <param name="rectWidth"></param>
    /// <param name="rotationRadians"></param>
    /// <returns></returns>
    /// <since>6.0</since>
    public static TextEntity CreateWithRichText(string richTextString, Plane plane, DimensionStyle style, bool wrapped, double rectWidth, double rotationRadians)
    {
      var const_ptr_dimstyle = style.ConstPointer();
      IntPtr ptr_text = UnsafeNativeMethods.ON_V6_TextObject_Create( richTextString, plane, const_ptr_dimstyle, wrapped, rectWidth, rotationRadians);
      if( IntPtr.Zero == ptr_text )
        return null;
      var rc = new TextEntity(ptr_text, null);
      rc.ParentDimensionStyle = style;
      return rc;
    }

    #region properties originating from dim style that can be overridden
    /// <summary>
    /// Gets or sets the justification of text in relation to its base point.
    /// </summary>
    /// <example>
    /// <code source='examples\vbnet\ex_textjustify.vb' lang='vbnet'/>
    /// <code source='examples\cs\ex_textjustify.cs' lang='cs'/>
    /// <code source='examples\py\ex_textjustify.py' lang='py'/>
    /// </example>
    /// <since>5.0</since>
    public TextJustification Justification
    {
      get
      {
        var horz = TextHorizontalAlignment;
        var vert = TextVerticalAlignment;
        TextJustification rc = TextJustification.None;
        switch(horz)
        {
          case TextHorizontalAlignment.Left:
            rc |= TextJustification.Left;
            break;
          case TextHorizontalAlignment.Center:
            rc |= TextJustification.Center;
            break;
          case TextHorizontalAlignment.Right:
            rc |= TextJustification.Right;
            break;
        }
        switch(vert)
        {
          case TextVerticalAlignment.Top:
            rc |= TextJustification.Top;
            break;
          case TextVerticalAlignment.MiddleOfTop:
            break;
          case TextVerticalAlignment.BottomOfTop:
            break;
          case TextVerticalAlignment.Middle:
            rc |= TextJustification.Middle;
            break;
          case TextVerticalAlignment.MiddleOfBottom:
            break;
          case TextVerticalAlignment.Bottom:
            rc |= TextJustification.Bottom;
            break;
          case TextVerticalAlignment.BottomOfBoundingBox:
            break;
        }
        return rc;
      }
      set
      {
        switch(value)
        {
          case TextJustification.Left:
            TextHorizontalAlignment = TextHorizontalAlignment.Left;
            break;
          case TextJustification.Center:
            TextHorizontalAlignment = TextHorizontalAlignment.Center;
            break;
          case TextJustification.Right:
            TextHorizontalAlignment = TextHorizontalAlignment.Right;
            break;
          case TextJustification.Bottom:
            TextVerticalAlignment = TextVerticalAlignment.Bottom;
            break;
          case TextJustification.Middle:
            TextVerticalAlignment = TextVerticalAlignment.Middle;
            break;
          case TextJustification.Top:
            TextVerticalAlignment = TextVerticalAlignment.Top;
            break;
          case TextJustification.BottomLeft:
            TextVerticalAlignment = TextVerticalAlignment.Bottom;
            TextHorizontalAlignment = TextHorizontalAlignment.Left;
            break;
          case TextJustification.BottomCenter:
            TextVerticalAlignment = TextVerticalAlignment.Bottom;
            TextHorizontalAlignment = TextHorizontalAlignment.Center;
            break;
          case TextJustification.BottomRight:
            TextVerticalAlignment = TextVerticalAlignment.Bottom;
            TextHorizontalAlignment = TextHorizontalAlignment.Right;
            break;
          case TextJustification.MiddleLeft:
            TextVerticalAlignment = TextVerticalAlignment.Middle;
            TextHorizontalAlignment = TextHorizontalAlignment.Left;
            break;
          case TextJustification.MiddleCenter:
            TextVerticalAlignment = TextVerticalAlignment.Middle;
            TextHorizontalAlignment = TextHorizontalAlignment.Center;
            break;
          case TextJustification.MiddleRight:
            TextVerticalAlignment = TextVerticalAlignment.Middle;
            TextHorizontalAlignment = TextHorizontalAlignment.Right;
            break;
          case TextJustification.TopLeft:
            TextVerticalAlignment = TextVerticalAlignment.Top;
            TextHorizontalAlignment = TextHorizontalAlignment.Left;
            break;
          case TextJustification.TopCenter:
            TextVerticalAlignment = TextVerticalAlignment.Top;
            TextHorizontalAlignment = TextHorizontalAlignment.Center;
            break;
          case TextJustification.TopRight:
            TextVerticalAlignment = TextVerticalAlignment.Top;
            TextHorizontalAlignment = TextHorizontalAlignment.Right;
            break;
        }
      }
    }

    /// <since>6.0</since>
    public TextHorizontalAlignment TextHorizontalAlignment
    {
      get
      {
        IntPtr thisptr = ConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        TextHorizontalAlignment rc =  UnsafeNativeMethods.ON_V6_Annotation_TextHorizontalAlignment(thisptr, styleptr);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
        return rc;
      }
      set
      {
        IntPtr thisptr = NonConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        UnsafeNativeMethods.ON_V6_Annotation_SetTextHorizontalAlignment(thisptr, styleptr, value);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
      }
    }

    /// <since>6.0</since>
    public TextVerticalAlignment TextVerticalAlignment
    {
      get
      {
        IntPtr thisptr = ConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        TextVerticalAlignment rc =  UnsafeNativeMethods.ON_V6_Annotation_TextVerticalAlignment(thisptr, styleptr);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
        return rc;
      }
      set
      {
        IntPtr thisptr = NonConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        UnsafeNativeMethods.ON_V6_Annotation_SetTextVerticalAlignment(thisptr, styleptr, value);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
      }
    }

    /// <since>6.0</since>
    public TextOrientation TextOrientation
    {
      get
      {
        IntPtr thisptr = ConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        TextOrientation rc = UnsafeNativeMethods.ON_Text_GetTextOrientation(thisptr, styleptr);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
        return rc;
      }
      set
      {
        IntPtr thisptr = NonConstPointer();
        IntPtr styleptr = ConstParentDimStylePointer();
        UnsafeNativeMethods.ON_Text_SetTextOrientation(thisptr, styleptr, value);
        GC.KeepAlive(this);   // GC_KeepAlive: Nov. 1, 2018
      }
    }

    #endregion properties originating from dim style that can be overridden

    /// <summary>
    /// Transform the object by a 4x4 transform matrix and change text height
    /// override to accommodate scaling in the transform if necessary
    /// </summary>
    /// <param name="transform">
    /// An Transform with the transformation information
    /// </param>
    /// <param name="style">
    /// </param>
    /// <returns>
    /// Returns true on success otherwise returns false.
    /// </returns>
    /// <since>6.0</since>
    public bool Transform(Transform transform, DimensionStyle style)
    {
      var style_pointer = style?.ConstPointer() ?? IntPtr.Zero;
      var pointer = NonConstPointer();
      var success = UnsafeNativeMethods.ON_V6_TextObject_Transform(pointer, ref transform, style_pointer);
      return success;
    }

    /// <summary> Get the transform for this text object's text geometry </summary>
    /// <param name="textscale"></param>
    /// <param name="dimstyle"></param>
    /// <since>6.0</since>
    [ConstOperation]
    public Transform GetTextTransform(double textscale, DimensionStyle dimstyle)
    {
      Transform xform = new Transform();
      IntPtr const_ptr_this = ConstPointer();
      IntPtr const_ptr_dimstyle = dimstyle.ConstPointer();
      UnsafeNativeMethods.ON_V6_TextObject_GetTextXform(const_ptr_this, const_ptr_dimstyle, textscale, ref xform);
      return xform;
    }

#if RHINO_SDK
    /// <summary>
    /// Explodes this text entity into an array of curves.
    /// </summary>
    /// <returns>An array of curves that forms the outline or content of this text entity.</returns>
    /// <since>5.0</since>
    [ConstOperation]
    public Curve[] Explode()
    {
      IntPtr const_ptr_parent = IntPtr.Zero;
      IntPtr const_ptr_this = IntPtr.Zero;
      IntPtr const_ptr_this_dimstyle = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        const_ptr_parent = parent.ConstPointer();
      else
      {
        const_ptr_this = ConstPointer();
        //const_ptr_this_dimstyle = ConstPointerForDimStyle();
      }

      var dimstyle = DimensionStyle;
      if(null == parent)
        const_ptr_this_dimstyle = dimstyle.ConstPointer();

      using (var curves = new Runtime.InteropWrappers.SimpleArrayCurvePointer())
      {
        IntPtr ptr_curves = curves.NonConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetTextOutlines(const_ptr_parent, const_ptr_this, const_ptr_this_dimstyle, ptr_curves);

        GC.KeepAlive(dimstyle);   // GC_KeepAlive: Nov. 1, 2018
        GC.KeepAlive(parent);   // GC_KeepAlive: Nov. 1, 2018

        return curves.ToNonConstArray();
      }
    }

    /// <summary>
    /// Creates planar breps from the outline curves.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="smallCapsScale"> Set to create small caps out of lower case letters.</param>
    /// <param name="spacing"> Set to add additional spacing between glyph output.</param>
    /// <returns>An array of planar breps.</returns>
    /// <since>6.0</since>
    [ConstOperation]
    public Brep[] CreateSurfaces(DimensionStyle dimstyle, double smallCapsScale = 1.0, double spacing = 0.0)
    {
      IntPtr const_ptr_parent = IntPtr.Zero;
      IntPtr const_ptr_this = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        const_ptr_parent = parent.ConstPointer();
      else
        const_ptr_this = ConstPointer();

      using (var breps = new Runtime.InteropWrappers.SimpleArrayBrepPointer())
      {
        IntPtr ptr_breps = breps.NonConstPointer();
        var const_ptr_dimstyle = dimstyle.ConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetPlanarBrepsFromText(const_ptr_parent, const_ptr_this, const_ptr_dimstyle, smallCapsScale, spacing, ptr_breps, IntPtr.Zero);
        return breps.ToNonConstArray();
      }
    }

    /// <summary>
    /// Creates planar Breps from text outline curves. Breps are grouped such that each element
    /// in the list being returned represents a single character.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="smallCapsScale"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    /// <since>8.0</since>
    [ConstOperation]
    public System.Collections.Generic.List<Brep[]> CreateSurfacesGrouped(DimensionStyle dimstyle, double smallCapsScale, double spacing)
    {
      IntPtr constPtrParent = IntPtr.Zero;
      IntPtr constPtrThis = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        constPtrParent = parent.ConstPointer();
      else
        constPtrThis = ConstPointer();

      using (var breps = new SimpleArrayBrepPointer())
      using (var indices = new SimpleArrayInt())
      {
        IntPtr ptrBreps = breps.NonConstPointer();
        IntPtr constPtrDimstyle = dimstyle.ConstPointer();
        IntPtr ptrIndices = indices.NonConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetPlanarBrepsFromText(constPtrParent, constPtrThis, constPtrDimstyle, smallCapsScale, spacing, ptrBreps, ptrIndices);
        GC.KeepAlive(dimstyle);
        int[] letterIndices = indices.ToArray();
        Brep[] brepArray = breps.ToNonConstArray();
        var dict = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Brep>>();

        int maxLetter = -1;
        for (int i = 0; i < letterIndices.Length; i++)
        {
          int letter = letterIndices[i];
          if (letter > maxLetter)
            maxLetter = letter;
          if (!dict.ContainsKey(letter))
            dict[letter] = new System.Collections.Generic.List<Brep>();
          dict[letter].Add(brepArray[i]);
        }

        var rc = new System.Collections.Generic.List<Brep[]>();
        for (int i=0; i<=maxLetter; i++)
        {
          if (dict.ContainsKey(i))
            rc.Add(dict[i].ToArray());
        }
        return rc;
      }
    }

    /// <summary>
    /// Creates 3d Breps from text outline curves. Breps are grouped such that each element
    /// in the list being returned represents a single character.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="smallCapsScale"></param>
    /// <param name="height"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    /// <since>8.0</since>
    [ConstOperation]
    public System.Collections.Generic.List<Brep[]> CreatePolysurfacesGrouped(DimensionStyle dimstyle, double smallCapsScale, double height, double spacing)
    {
      IntPtr constPtrParent = IntPtr.Zero;
      IntPtr constPtrThis = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        constPtrParent = parent.ConstPointer();
      else
        constPtrThis = ConstPointer();

      using (var breps = new SimpleArrayBrepPointer())
      using (var indices = new SimpleArrayInt())
      {
        IntPtr ptrBreps = breps.NonConstPointer();
        IntPtr constPtrDimstyle = dimstyle.ConstPointer();
        IntPtr ptrIndices = indices.NonConstPointer();
        UnsafeNativeMethods.RHC_RhinoGet3dBrepsFromText(constPtrParent, constPtrThis, constPtrDimstyle, smallCapsScale, height, spacing, ptrBreps, ptrIndices);
        GC.KeepAlive(dimstyle);
        int[] letterIndices = indices.ToArray();
        Brep[] brepArray = breps.ToNonConstArray();
        var dict = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Brep>>();

        int maxLetter = -1;
        for (int i = 0; i < letterIndices.Length; i++)
        {
          int letter = letterIndices[i];
          if (letter > maxLetter)
            maxLetter = letter;
          if (!dict.ContainsKey(letter))
            dict[letter] = new System.Collections.Generic.List<Brep>();
          dict[letter].Add(brepArray[i]);
        }

        var rc = new System.Collections.Generic.List<Brep[]>();
        for (int i = 0; i <= maxLetter; i++)
        {
          if (dict.ContainsKey(i))
            rc.Add(dict[i].ToArray());
        }
        return rc;
      }
    }

    /// <summary>
    /// Creates breps from the outline curves with specified height.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="height"> Height in direction perpendicular to plane of text.</param>
    /// <param name="smallCapsScale"> Set to create small caps out of lower case letters.</param>
    /// <param name="spacing"> Set to add additional spacing between glyph output.</param>
    /// <returns>An array of planar breps.</returns>
    /// <since>6.0</since>
    [ConstOperation]
    public Brep[] CreatePolySurfaces(DimensionStyle dimstyle, double height, double smallCapsScale = 1.0, double spacing = 0.0)
    {
      IntPtr const_ptr_parent = IntPtr.Zero;
      IntPtr const_ptr_this = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        const_ptr_parent = parent.ConstPointer();
      else
        const_ptr_this = ConstPointer();

      using (var breps = new Runtime.InteropWrappers.SimpleArrayBrepPointer())
      {
        IntPtr ptr_breps = breps.NonConstPointer();
        var const_ptr_dimstyle = dimstyle.ConstPointer();
        UnsafeNativeMethods.RHC_RhinoGet3dBrepsFromText(const_ptr_parent, const_ptr_this, const_ptr_dimstyle, smallCapsScale, height, spacing, ptr_breps, IntPtr.Zero);
        return breps.ToNonConstArray();
      }
    }


    /// <summary>
    /// Creates 3d Breps from text outline curves. Breps are grouped such that each element
    /// in the list being returned represents a single character.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="smallCapsScale"></param>
    /// <param name="height"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    /// <since>8.0</since>
    [ConstOperation]
    public System.Collections.Generic.List<Extrusion[]> CreateExtrusionsGrouped(DimensionStyle dimstyle, double smallCapsScale, double height, double spacing)
    {
      IntPtr constPtrParent = IntPtr.Zero;
      IntPtr constPtrThis = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        constPtrParent = parent.ConstPointer();
      else
        constPtrThis = ConstPointer();

      using (var extrusions = new SimpleArrayExtrusionPointer())
      using (var indices = new SimpleArrayInt())
      {
        IntPtr ptrExtrusions = extrusions.NonConstPointer();
        IntPtr constPtrDimstyle = dimstyle.ConstPointer();
        IntPtr ptrIndices = indices.NonConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetExtrusionsFromText(constPtrParent, constPtrThis, constPtrDimstyle, smallCapsScale, height, spacing, ptrExtrusions, ptrIndices);
        GC.KeepAlive(dimstyle);
        int[] letterIndices = indices.ToArray();
        Extrusion[] extrusionArray = extrusions.ToNonConstArray();
        var dict = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Extrusion>>();

        int maxLetter = -1;
        for (int i = 0; i < letterIndices.Length; i++)
        {
          int letter = letterIndices[i];
          if (letter > maxLetter)
            maxLetter = letter;
          if (!dict.ContainsKey(letter))
            dict[letter] = new System.Collections.Generic.List<Extrusion>();
          dict[letter].Add(extrusionArray[i]);
        }

        var rc = new System.Collections.Generic.List<Extrusion[]>();
        for (int i = 0; i <= maxLetter; i++)
        {
          if (dict.ContainsKey(i))
            rc.Add(dict[i].ToArray());
        }
        return rc;
      }
    }

    /// <summary>
    /// Creates extrusions from the outline curves with specified height.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="height"> Height in direction perpendicular to plane of text.</param>
    /// <param name="smallCapsScale"> Set to create small caps out of lower case letters.</param>
    /// <param name="spacing"> Set to add additional spacing between glyph output.</param>
    /// <returns>An array of planar breps.</returns>
    /// <since>6.0</since>
    [ConstOperation]
    public Extrusion[] CreateExtrusions(DimensionStyle dimstyle, double height, double smallCapsScale = 1.0, double spacing = 0.0)
    {
      IntPtr const_ptr_parent = IntPtr.Zero;
      IntPtr const_ptr_this = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        const_ptr_parent = parent.ConstPointer();
      else
        const_ptr_this = ConstPointer();

      using (var extrusions = new Runtime.InteropWrappers.SimpleArrayExtrusionPointer())
      {
        IntPtr ptr_extrusions = extrusions.NonConstPointer();
        var const_ptr_dimstyle = dimstyle.ConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetExtrusionsFromText(const_ptr_parent, const_ptr_this, const_ptr_dimstyle, smallCapsScale, height, spacing, ptr_extrusions, IntPtr.Zero);
        return extrusions.ToNonConstArray();
      }
    }

    /// <summary>
    /// Returns the outline curves.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="allowOpen"> Set to true to prevent forced closing of open curves retrieved from glyphs.</param>
    /// <param name="smallCapsScale"> Set to create small caps out of lower case letters.</param>
    /// <param name="spacing"> Set to add additional spacing between glyph output.</param>
    /// <returns>An array of curves that forms the outline or content of this text entity.</returns>
    /// <since>6.0</since>
    [ConstOperation]
    public Curve[] CreateCurves(DimensionStyle dimstyle, bool allowOpen, double smallCapsScale = 1.0, double spacing = 0.0)
    {
      IntPtr const_ptr_parent = IntPtr.Zero;
      IntPtr const_ptr_this = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        const_ptr_parent = parent.ConstPointer();
      else
        const_ptr_this = ConstPointer();

      using (var curves = new SimpleArrayCurvePointer())
      {
        IntPtr ptr_curves = curves.NonConstPointer();
        var const_ptr_dimstyle = dimstyle.ConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetPlanarCurvesFromText(const_ptr_parent, const_ptr_this, const_ptr_dimstyle, !allowOpen, smallCapsScale, spacing, ptr_curves, IntPtr.Zero);
        GC.KeepAlive(dimstyle);
        return curves.ToNonConstArray();
      }
    }

    /// <summary>
    /// Creates planar curve from text outline curves. Curves are grouped such that each element
    /// in the list being returned represents a single character.
    /// </summary>
    /// <param name="dimstyle"></param>
    /// <param name="allowOpen"> Set to true to prevent forced closing of open curves retrieved from glyphs.</param>
    /// <param name="smallCapsScale"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    /// <since>8.0</since>
    public System.Collections.Generic.List<Curve[]> CreateCurvesGrouped(DimensionStyle dimstyle, bool allowOpen, double smallCapsScale, double spacing)
    {
      IntPtr constPtrParent = IntPtr.Zero;
      IntPtr constPtrThis = IntPtr.Zero;

      TextObject parent = _GetConstObjectParent() as TextObject;
      if (null != parent)
        constPtrParent = parent.ConstPointer();
      else
        constPtrThis = ConstPointer();

      using (var curves = new SimpleArrayCurvePointer())
      using (var indices = new SimpleArrayInt())
      {
        IntPtr ptrCurves = curves.NonConstPointer();
        IntPtr constPtrDimstyle = dimstyle.ConstPointer();
        IntPtr ptrIndices = indices.NonConstPointer();
        UnsafeNativeMethods.RHC_RhinoGetPlanarCurvesFromText(constPtrParent, constPtrThis, constPtrDimstyle, !allowOpen, smallCapsScale, spacing, ptrCurves, ptrIndices);
        GC.KeepAlive(dimstyle);

        Curve[] curveArray = curves.ToNonConstArray();
        int[] letterIndices = indices.ToArray();
        var dict = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Curve>>();

        int maxLetter = -1;
        for (int i = 0; i < letterIndices.Length; i++)
        {
          int letter = letterIndices[i];
          if (letter > maxLetter)
            maxLetter = letter;
          if (!dict.ContainsKey(letter))
            dict[letter] = new System.Collections.Generic.List<Curve>();
          dict[letter].Add(curveArray[i]);
        }

        var rc = new System.Collections.Generic.List<Curve[]>();
        for (int i = 0; i <= maxLetter; i++)
        {
          if (dict.ContainsKey(i))
            rc.Add(dict[i].ToArray());
        }
        return rc;
      }
    }
#endif

  }
}
