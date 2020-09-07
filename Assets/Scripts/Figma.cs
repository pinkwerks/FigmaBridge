// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do one of these:
//
//    using QuickType;
//
//    var fileResponse = FileResponse.FromJson(jsonString);
//    var commentsResponse = CommentsResponse.FromJson(jsonString);
//    var commentRequest = CommentRequest.FromJson(jsonString);
//    var projectsResponse = ProjectsResponse.FromJson(jsonString);
//    var projectFilesResponse = ProjectFilesResponse.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// GET /v1/files/:key
    ///
    /// > Description
    ///
    /// Returns the document refered to by :key as a JSON object. The file key can be parsed from
    /// any Figma file url: https://www.figma.com/file/:key/:title. The "document" attribute
    /// contains a Node of type DOCUMENT.
    ///
    /// The "components" key contains a mapping from node IDs to component metadata. This is to
    /// help you determine which components each instance comes from. Currently the only piece of
    /// metadata available on components is the name of the component, but more properties will
    /// be forthcoming.
    ///
    /// > Path parameters
    ///
    /// key String
    /// File to export JSON from
    /// </summary>
    public partial class FileResponse
    {
        /// <summary>
        /// A mapping from node IDs to component metadata. This is to help you determine which
        /// components each instance comes from. Currently the only piece of metadata available on
        /// components is the name of the component, but more properties will be forthcoming.
        /// </summary>
        [JsonProperty("components")]
        public Dictionary<string, Component> Components { get; set; }

        /// <summary>
        /// The root node within the document
        /// </summary>
        [JsonProperty("document")]
        public DocumentClass Document { get; set; }

        [JsonProperty("schemaVersion")]
        public float SchemaVersion { get; set; }
    }

    /// <summary>
    /// A node that can have instances created of it that share the same properties
    /// A description of a master component. Helps you identify which component
    /// instances are attached to
    /// </summary>
    public partial class Component
    {
        /// <summary>
        /// Bounding box of the node in absolute space coordinates
        /// </summary>
        [JsonProperty("absoluteBoundingBox")]
        public Rect AbsoluteBoundingBox { get; set; }

        /// <summary>
        /// Background color of the node
        /// </summary>
        [JsonProperty("backgroundColor")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// How this node blends with nodes behind it in the scene
        /// (see blend mode section for more details)
        /// </summary>
        [JsonProperty("blendMode")]
        public BlendMode BlendMode { get; set; }

        /// <summary>
        /// An array of nodes that are direct children of this node
        /// </summary>
        [JsonProperty("children")]
        public Document[] Children { get; set; }

        /// <summary>
        /// Does this node clip content outside of its bounds?
        /// </summary>
        [JsonProperty("clipsContent")]
        public bool ClipsContent { get; set; }

        /// <summary>
        /// Horizontal and vertical layout constraints for node
        /// </summary>
        [JsonProperty("constraints")]
        public LayoutConstraint Constraints { get; set; }

        /// <summary>
        /// The description of the component as entered in the editor
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// An array of effects attached to this node
        /// (see effects section for more details)
        /// </summary>
        [JsonProperty("effects")]
        public Effect[] Effects { get; set; }

        /// <summary>
        /// An array of export settings representing images to export from node
        /// </summary>
        [JsonProperty("exportSettings")]
        public ExportSetting[] ExportSettings { get; set; }

        /// <summary>
        /// a string uniquely identifying this node within the document
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Does this node mask sibling nodes in front of it?
        /// </summary>
        [JsonProperty("isMask")]
        public bool IsMask { get; set; }

        /// <summary>
        /// An array of layout grids attached to this node (see layout grids section
        /// for more details). GROUP nodes do not have this attribute
        /// </summary>
        [JsonProperty("layoutGrids")]
        public LayoutGrid[] LayoutGrids { get; set; }

        /// <summary>
        /// The name of the component
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Opacity of the node
        /// </summary>
        [JsonProperty("opacity")]
        public float Opacity { get; set; }

        /// <summary>
        /// Keep height and width constrained to same ratio
        /// </summary>
        [JsonProperty("preserveRatio")]
        public bool PreserveRatio { get; set; }

        /// <summary>
        /// Node ID of node to transition to in prototyping
        /// </summary>
        [JsonProperty("transitionNodeID")]
        public string TransitionNodeId { get; set; }

        /// <summary>
        /// the type of the node, refer to table below for details
        /// </summary>
        [JsonProperty("type")]
        public NodeType Type { get; set; }

        /// <summary>
        /// whether or not the node is visible on the canvas
        /// </summary>
        ///
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// Bounding box of the node in absolute space coordinates
    ///
    /// A rectangle that expresses a bounding box in absolute coordinates
    /// </summary>
    public partial class Rect
    {
        /// <summary>
        /// Height of the rectangle
        /// </summary>
        [JsonProperty("height")]
        public float Height { get; set; }

        /// <summary>
        /// Width of the rectangle
        /// </summary>
        [JsonProperty("width")]
        public float Width { get; set; }

        /// <summary>
        /// X coordinate of top left corner of the rectangle
        /// </summary>
        [JsonProperty("x")]
        public float X { get; set; }

        /// <summary>
        /// Y coordinate of top left corner of the rectangle
        /// </summary>
        [JsonProperty("y")]
        public float Y { get; set; }
    }

    /// <summary>
    /// Background color of the node
    ///
    /// An RGBA color
    ///
    /// Background color of the canvas
    ///
    /// Solid color of the paint
    ///
    /// Color attached to corresponding position
    ///
    /// Color of the grid
    /// </summary>
    public partial class Color
    {
        /// <summary>
        /// Alpha channel value, between 0 and 1
        /// </summary>
        [JsonProperty("a")]
        public float A { get; set; }

        /// <summary>
        /// Blue channel value, between 0 and 1
        /// </summary>
        [JsonProperty("b")]
        public float B { get; set; }

        /// <summary>
        /// Green channel value, between 0 and 1
        /// </summary>
        [JsonProperty("g")]
        public float G { get; set; }

        /// <summary>
        /// Red channel value, between 0 and 1
        /// </summary>
        [JsonProperty("r")]
        public float R { get; set; }
    }

    /// <summary>
    /// Node Properties
    /// The root node
    ///
    /// The root node within the document
    ///
    /// Represents a single page
    ///
    /// A node of fixed size containing other nodes
    ///
    /// A logical grouping of nodes
    ///
    /// A vector network, consisting of vertices and edges
    ///
    /// A group that has a boolean operation applied to it
    ///
    /// A regular star shape
    ///
    /// A straight line
    ///
    /// An ellipse
    ///
    /// A regular n-sided polygon
    ///
    /// A rectangle
    ///
    /// A text box
    ///
    /// A rectangular region of the canvas that can be exported
    ///
    /// A node that can have instances created of it that share the same properties
    /// A description of a master component. Helps you identify which component
    /// instances are attached to
    ///
    /// An instance of a component, changes to the component result in the same
    /// changes applied to the instance
    /// </summary>
    public partial class Document
    {
        /// <summary>
        /// An array of canvases attached to the document
        ///
        /// An array of top level layers on the canvas
        ///
        /// An array of nodes that are direct children of this node
        ///
        /// An array of nodes that are being boolean operated on
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public Document[] Children { get; set; }

        /// <summary>
        /// a string uniquely identifying this node within the document
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// the name given to the node by the user in the tool.
        ///
        /// The name of the component
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// the type of the node, refer to table below for details
        /// </summary>
        [JsonProperty("type")]
        public NodeType Type { get; set; }

        /// <summary>
        /// whether or not the node is visible on the canvas
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }

        /// <summary>
        /// Background color of the canvas
        ///
        /// Background color of the node
        /// </summary>
        [JsonProperty("backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// An array of export settings representing images to export from the canvas
        ///
        /// An array of export settings representing images to export from node
        ///
        /// An array of export settings representing images to export from this node
        /// </summary>
        [JsonProperty("exportSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExportSetting[] ExportSettings { get; set; }

        /// <summary>
        /// Bounding box of the node in absolute space coordinates
        /// </summary>
        [JsonProperty("absoluteBoundingBox", NullValueHandling = NullValueHandling.Ignore)]
        public Rect AbsoluteBoundingBox { get; set; }

        /// <summary>
        /// How this node blends with nodes behind it in the scene
        /// (see blend mode section for more details)
        /// </summary>
        [JsonProperty("blendMode", NullValueHandling = NullValueHandling.Ignore)]
        public BlendMode? BlendMode { get; set; }

        /// <summary>
        /// Does this node clip content outside of its bounds?
        /// </summary>
        [JsonProperty("clipsContent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ClipsContent { get; set; }

        /// <summary>
        /// Horizontal and vertical layout constraints for node
        /// </summary>
        [JsonProperty("constraints", NullValueHandling = NullValueHandling.Ignore)]
        public LayoutConstraint Constraints { get; set; }

        /// <summary>
        /// An array of effects attached to this node
        /// (see effects section for more details)
        /// </summary>
        [JsonProperty("effects", NullValueHandling = NullValueHandling.Ignore)]
        public Effect[] Effects { get; set; }

        /// <summary>
        /// Does this node mask sibling nodes in front of it?
        /// </summary>
        [JsonProperty("isMask", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMask { get; set; }

        /// <summary>
        /// An array of layout grids attached to this node (see layout grids section
        /// for more details). GROUP nodes do not have this attribute
        /// </summary>
        [JsonProperty("layoutGrids", NullValueHandling = NullValueHandling.Ignore)]
        public LayoutGrid[] LayoutGrids { get; set; }

        /// <summary>
        /// Opacity of the node
        /// </summary>
        [JsonProperty("opacity", NullValueHandling = NullValueHandling.Ignore)]
        public float? Opacity { get; set; }

        /// <summary>
        /// Keep height and width constrained to same ratio
        /// </summary>
        [JsonProperty("preserveRatio", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PreserveRatio { get; set; }

        /// <summary>
        /// Node ID of node to transition to in prototyping
        /// </summary>
        [JsonProperty("transitionNodeID")]
        public string TransitionNodeId { get; set; }

        /// <summary>
        /// An array of fill paints applied to the node
        /// </summary>
        [JsonProperty("fills", NullValueHandling = NullValueHandling.Ignore)]
        public Paint[] Fills { get; set; }

        /// <summary>
        /// Where stroke is drawn relative to the vector outline as a string enum
        /// "INSIDE": draw stroke inside the shape boundary
        /// "OUTSIDE": draw stroke outside the shape boundary
        /// "CENTER": draw stroke centered along the shape boundary
        /// </summary>
        [JsonProperty("strokeAlign", NullValueHandling = NullValueHandling.Ignore)]
        public StrokeAlign? StrokeAlign { get; set; }

        /// <summary>
        /// An array of stroke paints applied to the node
        /// </summary>
        [JsonProperty("strokes", NullValueHandling = NullValueHandling.Ignore)]
        public Paint[] Strokes { get; set; }

        /// <summary>
        /// The weight of strokes on the node
        /// </summary>
        [JsonProperty("strokeWeight", NullValueHandling = NullValueHandling.Ignore)]
        public float? StrokeWeight { get; set; }

        /// <summary>
        /// Radius of each corner of the rectangle
        /// </summary>
        [JsonProperty("cornerRadius", NullValueHandling = NullValueHandling.Ignore)]
        public float? CornerRadius { get; set; }

        /// <summary>
        /// Text contained within text box
        /// </summary>
        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public string Characters { get; set; }

        /// <summary>
        /// Array with same number of elements as characeters in text box,
        /// each element is a reference to the styleOverrideTable defined
        /// below and maps to the corresponding character in the characters
        /// field. Elements with value 0 have the default type style
        /// </summary>
        [JsonProperty("characterStyleOverrides", NullValueHandling = NullValueHandling.Ignore)]
        public float[] CharacterStyleOverrides { get; set; }

        /// <summary>
        /// Style of text including font family and weight (see type style
        /// section for more information)
        /// </summary>
        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public TypeStyle Style { get; set; }

        /// <summary>
        /// Map from ID to TypeStyle for looking up style overrides
        /// </summary>
        [JsonProperty("styleOverrideTable", NullValueHandling = NullValueHandling.Ignore)]
        public TypeStyle StyleOverrideTable { get; set; }

        /// <summary>
        /// The description of the component as entered in the editor
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// ID of component that this instance came from, refers to components
        /// table (see endpoints section below)
        /// </summary>
        [JsonProperty("componentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentId { get; set; }
    }

    /// <summary>
    /// Horizontal and vertical layout constraints for node
    ///
    /// Layout constraint relative to containing Frame
    /// </summary>
    public partial class LayoutConstraint
    {
        /// <summary>
        /// Horizontal constraint as an enum
        /// "LEFT": Node is laid out relative to left of the containing frame
        /// "RIGHT": Node is laid out relative to right of the containing frame
        /// "CENTER": Node is horizontally centered relative to containing frame
        /// "LEFT_RIGHT": Both left and right of node are constrained relative to containing frame
        /// (node stretches with frame)
        /// "SCALE": Node scales horizontally with containing frame
        /// </summary>
        [JsonProperty("horizontal")]
        public Horizontal Horizontal { get; set; }

        /// <summary>
        /// Vertical constraint as an enum
        /// "TOP": Node is laid out relative to top of the containing frame
        /// "BOTTOM": Node is laid out relative to bottom of the containing frame
        /// "CENTER": Node is vertically centered relative to containing frame
        /// "TOP_BOTTOM": Both top and bottom of node are constrained relative to containing frame
        /// (node stretches with frame)
        /// "SCALE": Node scales vertically with containing frame
        /// </summary>
        [JsonProperty("vertical")]
        public Vertical Vertical { get; set; }
    }

    /// <summary>
    /// An array of effects attached to this node
    /// (see effects section for more details)
    ///
    /// A visual effect such as a shadow or blur
    /// </summary>
    public partial class Effect
    {
        /// <summary>
        /// Enum describing how layer blends with layers below
        /// This type is a string enum with the following possible values
        /// </summary>
        [JsonProperty("blendMode", NullValueHandling = NullValueHandling.Ignore)]
        public BlendMode? BlendMode { get; set; }

        /// <summary>
        /// An RGBA color
        /// </summary>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public Color Color { get; set; }

        /// <summary>
        /// A 2d vector
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Radius of the blur effect (applies to shadows as well)
        /// </summary>
        [JsonProperty("radius")]
        public float Radius { get; set; }

        /// <summary>
        /// Type of effect as a string enum
        /// </summary>
        [JsonProperty("type")]
        public EffectType Type { get; set; }

        /// <summary>
        /// Is the effect active?
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// A 2d vector
    ///
    /// This field contains three vectors, each of which are a position in
    /// normalized object space (normalized object space is if the top left
    /// corner of the bounding box of the object is (0, 0) and the bottom
    /// right is (1,1)). The first position corresponds to the start of the
    /// gradient (value 0 for the purposes of calculating gradient stops),
    /// the second position is the end of the gradient (value 1), and the
    /// third handle position determines the width of the gradient (only
    /// relevant for non-linear gradients).
    ///
    /// 2d vector offset within the frame.
    /// </summary>
    public partial class Vector2
    {
        /// <summary>
        /// X coordinate of the vector
        /// </summary>
        [JsonProperty("x")]
        public float X { get; set; }

        /// <summary>
        /// Y coordinate of the vector
        /// </summary>
        [JsonProperty("y")]
        public float Y { get; set; }
    }

    /// <summary>
    /// An array of export settings representing images to export from node
    ///
    /// Format and size to export an asset at
    ///
    /// An array of export settings representing images to export from this node
    ///
    /// An array of export settings representing images to export from the canvas
    /// </summary>
    public partial class ExportSetting
    {
        /// <summary>
        /// Constraint that determines sizing of exported asset
        /// </summary>
        [JsonProperty("constraint")]
        public Constraint Constraint { get; set; }

        /// <summary>
        /// Image type, string enum
        /// </summary>
        [JsonProperty("format")]
        public Format Format { get; set; }

        /// <summary>
        /// File suffix to append to all filenames
        /// </summary>
        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }

    /// <summary>
    /// Constraint that determines sizing of exported asset
    ///
    /// Sizing constraint for exports
    /// </summary>
    public partial class Constraint
    {
        /// <summary>
        /// Type of constraint to apply; string enum with potential values below
        /// "SCALE": Scale by value
        /// "WIDTH": Scale proportionally and set width to value
        /// "HEIGHT": Scale proportionally and set height to value
        /// </summary>
        [JsonProperty("type")]
        public ConstraintType Type { get; set; }

        /// <summary>
        /// See type property for effect of this field
        /// </summary>
        [JsonProperty("value")]
        public float Value { get; set; }
    }

    /// <summary>
    /// An array of fill paints applied to the node
    ///
    /// A solid color, gradient, or image texture that can be applied as fills or strokes
    ///
    /// An array of stroke paints applied to the node
    ///
    /// Paints applied to characters
    /// </summary>
    public partial class Paint
    {
        /// <summary>
        /// Solid color of the paint
        /// </summary>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public Color Color { get; set; }

        /// <summary>
        /// This field contains three vectors, each of which are a position in
        /// normalized object space (normalized object space is if the top left
        /// corner of the bounding box of the object is (0, 0) and the bottom
        /// right is (1,1)). The first position corresponds to the start of the
        /// gradient (value 0 for the purposes of calculating gradient stops),
        /// the second position is the end of the gradient (value 1), and the
        /// third handle position determines the width of the gradient (only
        /// relevant for non-linear gradients).
        /// </summary>
        [JsonProperty("gradientHandlePositions", NullValueHandling = NullValueHandling.Ignore)]
        public Vector2[] GradientHandlePositions { get; set; }

        /// <summary>
        /// Positions of key points along the gradient axis with the colors
        /// anchored there. Colors along the gradient are interpolated smoothly
        /// between neighboring gradient stops.
        /// </summary>
        [JsonProperty("gradientStops", NullValueHandling = NullValueHandling.Ignore)]
        public ColorStop[] GradientStops { get; set; }

        /// <summary>
        /// Overall opacity of paint (colors within the paint can also have opacity
        /// values which would blend with this)
        /// </summary>
        [JsonProperty("opacity")]
        public float Opacity { get; set; }

        /// <summary>
        /// Image scaling mode
        /// </summary>
        [JsonProperty("scaleMode", NullValueHandling = NullValueHandling.Ignore)]
        public string ScaleMode { get; set; }

        /// <summary>
        /// Type of paint as a string enum
        /// </summary>
        [JsonProperty("type")]
        public FillType Type { get; set; }

        /// <summary>
        /// Is the paint enabled?
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// Positions of key points along the gradient axis with the colors
    /// anchored there. Colors along the gradient are interpolated smoothly
    /// between neighboring gradient stops.
    ///
    /// A position color pair representing a gradient stop
    /// </summary>
    public partial class ColorStop
    {
        /// <summary>
        /// Color attached to corresponding position
        /// </summary>
        [JsonProperty("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Value between 0 and 1 representing position along gradient axis
        /// </summary>
        [JsonProperty("position")]
        public float Position { get; set; }
    }

    /// <summary>
    /// An array of layout grids attached to this node (see layout grids section
    /// for more details). GROUP nodes do not have this attribute
    ///
    /// Guides to align and place objects within a frame
    /// </summary>
    public partial class LayoutGrid
    {
        /// <summary>
        /// Positioning of grid as a string enum
        /// "MIN": Grid starts at the left or top of the frame
        /// "MAX": Grid starts at the right or bottom of the frame
        /// "CENTER": Grid is center aligned
        /// </summary>
        [JsonProperty("alignment")]
        public Alignment Alignment { get; set; }

        /// <summary>
        /// Color of the grid
        /// </summary>
        [JsonProperty("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Number of columns or rows
        /// </summary>
        [JsonProperty("count")]
        public float Count { get; set; }

        /// <summary>
        /// Spacing in between columns and rows
        /// </summary>
        [JsonProperty("gutterSize")]
        public float GutterSize { get; set; }

        /// <summary>
        /// Spacing before the first column or row
        /// </summary>
        [JsonProperty("offset")]
        public float Offset { get; set; }

        /// <summary>
        /// Orientation of the grid as a string enum
        /// "COLUMNS": Vertical grid
        /// "ROWS": Horizontal grid
        /// "GRID": Square grid
        /// </summary>
        [JsonProperty("pattern")]
        public Pattern Pattern { get; set; }

        /// <summary>
        /// Width of column grid or height of row grid or square grid spacing
        /// </summary>
        [JsonProperty("sectionSize")]
        public float SectionSize { get; set; }

        /// <summary>
        /// Is the grid currently visible?
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// Style of text including font family and weight (see type style
    /// section for more information)
    ///
    /// Metadata for character formatting
    ///
    /// Map from ID to TypeStyle for looking up style overrides
    /// </summary>
    public partial class TypeStyle
    {
        /// <summary>
        /// Paints applied to characters
        /// </summary>
        [JsonProperty("fills")]
        public Paint[] Fills { get; set; }

        /// <summary>
        /// Font family of text (standard name)
        /// </summary>
        [JsonProperty("fontFamily")]
        public string FontFamily { get; set; }

        /// <summary>
        /// PostScript font name
        /// </summary>
        [JsonProperty("fontPostScriptName")]
        public string FontPostScriptName { get; set; }

        /// <summary>
        /// Font size in px
        /// </summary>
        [JsonProperty("fontSize")]
        public float FontSize { get; set; }

        /// <summary>
        /// Numeric font weight
        /// </summary>
        [JsonProperty("fontWeight")]
        public float FontWeight { get; set; }

        /// <summary>
        /// Is text italicized?
        /// </summary>
        [JsonProperty("italic")]
        public bool Italic { get; set; }

        /// <summary>
        /// Space between characters in px
        /// </summary>
        [JsonProperty("letterSpacing")]
        public float LetterSpacing { get; set; }

        /// <summary>
        /// Line height as a percentage of normal line height
        /// </summary>
        [JsonProperty("lineHeightPercent")]
        public float LineHeightPercent { get; set; }

        /// <summary>
        /// Line height in px
        /// </summary>
        [JsonProperty("lineHeightPx")]
        public float LineHeightPx { get; set; }

        /// <summary>
        /// Horizontal text alignment as string enum
        /// </summary>
        [JsonProperty("textAlignHorizontal")]
        public TextAlignHorizontal TextAlignHorizontal { get; set; }

        /// <summary>
        /// Vertical text alignment as string enum
        /// </summary>
        [JsonProperty("textAlignVertical")]
        public TextAlignVertical TextAlignVertical { get; set; }
    }

    /// <summary>
    /// Node Properties
    /// The root node
    ///
    /// The root node within the document
    /// </summary>
    public partial class DocumentClass
    {
        /// <summary>
        /// An array of canvases attached to the document
        /// </summary>
        [JsonProperty("children")]
        public Document[] Children { get; set; }

        /// <summary>
        /// a string uniquely identifying this node within the document
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// the name given to the node by the user in the tool.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// the type of the node, refer to table below for details
        /// </summary>
        [JsonProperty("type")]
        public NodeType Type { get; set; }

        /// <summary>
        /// whether or not the node is visible on the canvas
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [JsonProperty("visible", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Visible { get; set; }
    }

    /// <summary>
    /// GET /v1/files/:key/comments
    ///
    /// > Description
    /// A list of comments left on the file.
    ///
    /// > Path parameters
    /// key String
    /// File to get comments from
    /// </summary>
    public partial class CommentsResponse
    {
        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }

    /// <summary>
    /// A comment or reply left by a user
    /// </summary>
    public partial class Comment
    {
        [JsonProperty("client_meta")]
        public ClientMeta ClientMeta { get; set; }

        /// <summary>
        /// The time at which the comment was left
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The file in which the comment lives
        /// </summary>
        [JsonProperty("file_key")]
        public string FileKey { get; set; }

        /// <summary>
        /// Unique identifier for comment
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// (MISSING IN DOCS)
        /// The content of the comment
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Only set for top level comments. The number displayed with the
        /// comment in the UI
        /// </summary>
        [JsonProperty("order_id")]
        public float OrderId { get; set; }

        /// <summary>
        /// If present, the id of the comment to which this is the reply
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// If set, when the comment was resolved
        /// </summary>
        [JsonProperty("resolved_at")]
        public DateTimeOffset? ResolvedAt { get; set; }

        /// <summary>
        /// The user who left the comment
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
    }

    /// <summary>
    /// A 2d vector
    ///
    /// This field contains three vectors, each of which are a position in
    /// normalized object space (normalized object space is if the top left
    /// corner of the bounding box of the object is (0, 0) and the bottom
    /// right is (1,1)). The first position corresponds to the start of the
    /// gradient (value 0 for the purposes of calculating gradient stops),
    /// the second position is the end of the gradient (value 1), and the
    /// third handle position determines the width of the gradient (only
    /// relevant for non-linear gradients).
    ///
    /// 2d vector offset within the frame.
    ///
    /// A relative offset within a frame
    /// </summary>
    public partial class ClientMeta
    {
        /// <summary>
        /// X coordinate of the vector
        /// </summary>
        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        public float? X { get; set; }

        /// <summary>
        /// Y coordinate of the vector
        /// </summary>
        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public float? Y { get; set; }

        /// <summary>
        /// Unique id specifying the frame.
        /// </summary>
        [JsonProperty("node_id", NullValueHandling = NullValueHandling.Ignore)]
        public string[] NodeId { get; set; }

        /// <summary>
        /// 2d vector offset within the frame.
        /// </summary>
        [JsonProperty("node_offset", NullValueHandling = NullValueHandling.Ignore)]
        public Vector2 NodeOffset { get; set; }
    }

    /// <summary>
    /// The user who left the comment
    ///
    /// A description of a user
    /// </summary>
    public partial class User
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("img_url")]
        public string ImgUrl { get; set; }
    }

    /// <summary>
    /// POST /v1/files/:key/comments
    ///
    /// > Description
    /// Posts a new comment on the file.
    ///
    /// > Path parameters
    /// key String
    /// File to get comments from
    ///
    /// > Body parameters
    /// message String
    /// The text contents of the comment to post
    ///
    /// client_meta Vector2 | FrameOffset
    /// The position of where to place the comment. This can either be an absolute canvas
    /// position or the relative position within a frame.
    ///
    /// > Return value
    /// The Comment that was successfully posted
    ///
    /// > Error codes
    /// 404 The specified file was not found
    /// </summary>
    public partial class CommentRequest
    {
        [JsonProperty("client_meta")]
        public ClientMeta ClientMeta { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// GET /v1/teams/:team_id/projects
    ///
    /// > Description
    /// Lists the projects for a specified team. Note that this will only return projects visible
    /// to the authenticated user or owner of the developer token.
    ///
    /// > Path parameters
    /// team_id String
    /// Id of the team to list projects from
    /// </summary>
    public partial class ProjectsResponse
    {
        [JsonProperty("projects")]
        public Project[] Projects { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("id")]
        public float Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// GET /v1/projects/:project_id/files
    ///
    /// > Description
    /// List the files in a given project.
    ///
    /// > Path parameters
    /// project_id String
    /// Id of the project to list files from
    /// </summary>
    public partial class ProjectFilesResponse
    {
        [JsonProperty("files")]
        public File[] Files { get; set; }
    }

    public partial class File
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// utc date in iso8601
        /// </summary>
        [JsonProperty("last_modified")]
        public string LastModified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
    }

    /// <summary>
    /// How this node blends with nodes behind it in the scene
    /// (see blend mode section for more details)
    ///
    /// Enum describing how layer blends with layers below
    /// This type is a string enum with the following possible values
    /// </summary>
    public enum BlendMode { Color, ColorBurn, ColorDodge, Darken, Difference, Exclusion, HardLight, Hue, Lighten, LinearBurn, LinearDodge, Luminosity, Multiply, Normal, Overlay, PassThrough, Saturation, Screen, SoftLight };

    /// <summary>
    /// Horizontal constraint as an enum
    /// "LEFT": Node is laid out relative to left of the containing frame
    /// "RIGHT": Node is laid out relative to right of the containing frame
    /// "CENTER": Node is horizontally centered relative to containing frame
    /// "LEFT_RIGHT": Both left and right of node are constrained relative to containing frame
    /// (node stretches with frame)
    /// "SCALE": Node scales horizontally with containing frame
    /// </summary>
    public enum Horizontal { Center, Left, LeftRight, Right, Scale };

    /// <summary>
    /// Vertical constraint as an enum
    /// "TOP": Node is laid out relative to top of the containing frame
    /// "BOTTOM": Node is laid out relative to bottom of the containing frame
    /// "CENTER": Node is vertically centered relative to containing frame
    /// "TOP_BOTTOM": Both top and bottom of node are constrained relative to containing frame
    /// (node stretches with frame)
    /// "SCALE": Node scales vertically with containing frame
    /// </summary>
    public enum Vertical { Bottom, Center, Scale, Top, TopBottom };

    /// <summary>
    /// Type of effect as a string enum
    /// </summary>
    public enum EffectType { BackgroundBlur, DropShadow, InnerShadow, LayerBlur };

    /// <summary>
    /// Type of constraint to apply; string enum with potential values below
    /// "SCALE": Scale by value
    /// "WIDTH": Scale proportionally and set width to value
    /// "HEIGHT": Scale proportionally and set height to value
    /// </summary>
    public enum ConstraintType { Height, Scale, Width };

    /// <summary>
    /// Image type, string enum
    /// </summary>
    public enum Format { Jpg, Png, Svg };

    /// <summary>
    /// Type of paint as a string enum
    /// </summary>
    public enum FillType { Emoji, GradientAngular, GradientDiamond, GradientLinear, GradientRadial, Image, Solid };

    /// <summary>
    /// Positioning of grid as a string enum
    /// "MIN": Grid starts at the left or top of the frame
    /// "MAX": Grid starts at the right or bottom of the frame
    /// "CENTER": Grid is center aligned
    /// </summary>
    public enum Alignment { Center, Max, Min };

    /// <summary>
    /// Orientation of the grid as a string enum
    /// "COLUMNS": Vertical grid
    /// "ROWS": Horizontal grid
    /// "GRID": Square grid
    /// </summary>
    public enum Pattern { Columns, Grid, Rows };

    /// <summary>
    /// Where stroke is drawn relative to the vector outline as a string enum
    /// "INSIDE": draw stroke inside the shape boundary
    /// "OUTSIDE": draw stroke outside the shape boundary
    /// "CENTER": draw stroke centered along the shape boundary
    /// </summary>
    public enum StrokeAlign { Center, Inside, Outside };

    /// <summary>
    /// Horizontal text alignment as string enum
    /// </summary>
    public enum TextAlignHorizontal { Center, Justified, Left, Right };

    /// <summary>
    /// Vertical text alignment as string enum
    /// </summary>
    public enum TextAlignVertical { Bottom, Center, Top };

    /// <summary>
    /// the type of the node, refer to table below for details
    /// </summary>
    public enum NodeType { Boolean, Canvas, Component, Document, Ellipse, Frame, Group, Instance, Line, Rectangle, RegularPolygon, Slice, Star, Text, Vector };

    public partial class FileResponse
    {
        public static FileResponse FromJson(string json) => JsonConvert.DeserializeObject<FileResponse>(json, QuickType.Converter.Settings);
    }

    public partial class CommentsResponse
    {
        public static CommentsResponse FromJson(string json) => JsonConvert.DeserializeObject<CommentsResponse>(json, QuickType.Converter.Settings);
    }

    public partial class CommentRequest
    {
        public static CommentRequest FromJson(string json) => JsonConvert.DeserializeObject<CommentRequest>(json, QuickType.Converter.Settings);
    }

    public partial class ProjectsResponse
    {
        public static ProjectsResponse FromJson(string json) => JsonConvert.DeserializeObject<ProjectsResponse>(json, QuickType.Converter.Settings);
    }

    public partial class ProjectFilesResponse
    {
        public static ProjectFilesResponse FromJson(string json) => JsonConvert.DeserializeObject<ProjectFilesResponse>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this FileResponse self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this CommentsResponse self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this CommentRequest self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this ProjectsResponse self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this ProjectFilesResponse self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                BlendModeConverter.Singleton,
                HorizontalConverter.Singleton,
                VerticalConverter.Singleton,
                EffectTypeConverter.Singleton,
                ConstraintTypeConverter.Singleton,
                FormatConverter.Singleton,
                FillTypeConverter.Singleton,
                AlignmentConverter.Singleton,
                PatternConverter.Singleton,
                StrokeAlignConverter.Singleton,
                TextAlignHorizontalConverter.Singleton,
                TextAlignVerticalConverter.Singleton,
                NodeTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class BlendModeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BlendMode) || t == typeof(BlendMode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "COLOR":
                    return BlendMode.Color;
                case "COLOR_BURN":
                    return BlendMode.ColorBurn;
                case "COLOR_DODGE":
                    return BlendMode.ColorDodge;
                case "DARKEN":
                    return BlendMode.Darken;
                case "DIFFERENCE":
                    return BlendMode.Difference;
                case "EXCLUSION":
                    return BlendMode.Exclusion;
                case "HARD_LIGHT":
                    return BlendMode.HardLight;
                case "HUE":
                    return BlendMode.Hue;
                case "LIGHTEN":
                    return BlendMode.Lighten;
                case "LINEAR_BURN":
                    return BlendMode.LinearBurn;
                case "LINEAR_DODGE":
                    return BlendMode.LinearDodge;
                case "LUMINOSITY":
                    return BlendMode.Luminosity;
                case "MULTIPLY":
                    return BlendMode.Multiply;
                case "NORMAL":
                    return BlendMode.Normal;
                case "OVERLAY":
                    return BlendMode.Overlay;
                case "PASS_THROUGH":
                    return BlendMode.PassThrough;
                case "SATURATION":
                    return BlendMode.Saturation;
                case "SCREEN":
                    return BlendMode.Screen;
                case "SOFT_LIGHT":
                    return BlendMode.SoftLight;
            }
            throw new Exception("Cannot unmarshal type BlendMode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BlendMode)untypedValue;
            switch (value)
            {
                case BlendMode.Color:
                    serializer.Serialize(writer, "COLOR");
                    return;
                case BlendMode.ColorBurn:
                    serializer.Serialize(writer, "COLOR_BURN");
                    return;
                case BlendMode.ColorDodge:
                    serializer.Serialize(writer, "COLOR_DODGE");
                    return;
                case BlendMode.Darken:
                    serializer.Serialize(writer, "DARKEN");
                    return;
                case BlendMode.Difference:
                    serializer.Serialize(writer, "DIFFERENCE");
                    return;
                case BlendMode.Exclusion:
                    serializer.Serialize(writer, "EXCLUSION");
                    return;
                case BlendMode.HardLight:
                    serializer.Serialize(writer, "HARD_LIGHT");
                    return;
                case BlendMode.Hue:
                    serializer.Serialize(writer, "HUE");
                    return;
                case BlendMode.Lighten:
                    serializer.Serialize(writer, "LIGHTEN");
                    return;
                case BlendMode.LinearBurn:
                    serializer.Serialize(writer, "LINEAR_BURN");
                    return;
                case BlendMode.LinearDodge:
                    serializer.Serialize(writer, "LINEAR_DODGE");
                    return;
                case BlendMode.Luminosity:
                    serializer.Serialize(writer, "LUMINOSITY");
                    return;
                case BlendMode.Multiply:
                    serializer.Serialize(writer, "MULTIPLY");
                    return;
                case BlendMode.Normal:
                    serializer.Serialize(writer, "NORMAL");
                    return;
                case BlendMode.Overlay:
                    serializer.Serialize(writer, "OVERLAY");
                    return;
                case BlendMode.PassThrough:
                    serializer.Serialize(writer, "PASS_THROUGH");
                    return;
                case BlendMode.Saturation:
                    serializer.Serialize(writer, "SATURATION");
                    return;
                case BlendMode.Screen:
                    serializer.Serialize(writer, "SCREEN");
                    return;
                case BlendMode.SoftLight:
                    serializer.Serialize(writer, "SOFT_LIGHT");
                    return;
            }
            throw new Exception("Cannot marshal type BlendMode");
        }

        public static readonly BlendModeConverter Singleton = new BlendModeConverter();
    }

    internal class HorizontalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Horizontal) || t == typeof(Horizontal?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CENTER":
                    return Horizontal.Center;
                case "LEFT":
                    return Horizontal.Left;
                case "LEFT_RIGHT":
                    return Horizontal.LeftRight;
                case "RIGHT":
                    return Horizontal.Right;
                case "SCALE":
                    return Horizontal.Scale;
            }
            throw new Exception("Cannot unmarshal type Horizontal");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Horizontal)untypedValue;
            switch (value)
            {
                case Horizontal.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case Horizontal.Left:
                    serializer.Serialize(writer, "LEFT");
                    return;
                case Horizontal.LeftRight:
                    serializer.Serialize(writer, "LEFT_RIGHT");
                    return;
                case Horizontal.Right:
                    serializer.Serialize(writer, "RIGHT");
                    return;
                case Horizontal.Scale:
                    serializer.Serialize(writer, "SCALE");
                    return;
            }
            throw new Exception("Cannot marshal type Horizontal");
        }

        public static readonly HorizontalConverter Singleton = new HorizontalConverter();
    }

    internal class VerticalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Vertical) || t == typeof(Vertical?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BOTTOM":
                    return Vertical.Bottom;
                case "CENTER":
                    return Vertical.Center;
                case "SCALE":
                    return Vertical.Scale;
                case "TOP":
                    return Vertical.Top;
                case "TOP_BOTTOM":
                    return Vertical.TopBottom;
            }
            throw new Exception("Cannot unmarshal type Vertical");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Vertical)untypedValue;
            switch (value)
            {
                case Vertical.Bottom:
                    serializer.Serialize(writer, "BOTTOM");
                    return;
                case Vertical.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case Vertical.Scale:
                    serializer.Serialize(writer, "SCALE");
                    return;
                case Vertical.Top:
                    serializer.Serialize(writer, "TOP");
                    return;
                case Vertical.TopBottom:
                    serializer.Serialize(writer, "TOP_BOTTOM");
                    return;
            }
            throw new Exception("Cannot marshal type Vertical");
        }

        public static readonly VerticalConverter Singleton = new VerticalConverter();
    }

    internal class EffectTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EffectType) || t == typeof(EffectType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BACKGROUND_BLUR":
                    return EffectType.BackgroundBlur;
                case "DROP_SHADOW":
                    return EffectType.DropShadow;
                case "INNER_SHADOW":
                    return EffectType.InnerShadow;
                case "LAYER_BLUR":
                    return EffectType.LayerBlur;
            }
            throw new Exception("Cannot unmarshal type EffectType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EffectType)untypedValue;
            switch (value)
            {
                case EffectType.BackgroundBlur:
                    serializer.Serialize(writer, "BACKGROUND_BLUR");
                    return;
                case EffectType.DropShadow:
                    serializer.Serialize(writer, "DROP_SHADOW");
                    return;
                case EffectType.InnerShadow:
                    serializer.Serialize(writer, "INNER_SHADOW");
                    return;
                case EffectType.LayerBlur:
                    serializer.Serialize(writer, "LAYER_BLUR");
                    return;
            }
            throw new Exception("Cannot marshal type EffectType");
        }

        public static readonly EffectTypeConverter Singleton = new EffectTypeConverter();
    }

    internal class ConstraintTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ConstraintType) || t == typeof(ConstraintType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "HEIGHT":
                    return ConstraintType.Height;
                case "SCALE":
                    return ConstraintType.Scale;
                case "WIDTH":
                    return ConstraintType.Width;
            }
            throw new Exception("Cannot unmarshal type ConstraintType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ConstraintType)untypedValue;
            switch (value)
            {
                case ConstraintType.Height:
                    serializer.Serialize(writer, "HEIGHT");
                    return;
                case ConstraintType.Scale:
                    serializer.Serialize(writer, "SCALE");
                    return;
                case ConstraintType.Width:
                    serializer.Serialize(writer, "WIDTH");
                    return;
            }
            throw new Exception("Cannot marshal type ConstraintType");
        }

        public static readonly ConstraintTypeConverter Singleton = new ConstraintTypeConverter();
    }

    internal class FormatConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Format) || t == typeof(Format?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "JPG":
                    return Format.Jpg;
                case "PNG":
                    return Format.Png;
                case "SVG":
                    return Format.Svg;
            }
            throw new Exception("Cannot unmarshal type Format");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Format)untypedValue;
            switch (value)
            {
                case Format.Jpg:
                    serializer.Serialize(writer, "JPG");
                    return;
                case Format.Png:
                    serializer.Serialize(writer, "PNG");
                    return;
                case Format.Svg:
                    serializer.Serialize(writer, "SVG");
                    return;
            }
            throw new Exception("Cannot marshal type Format");
        }

        public static readonly FormatConverter Singleton = new FormatConverter();
    }

    internal class FillTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FillType) || t == typeof(FillType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "EMOJI":
                    return FillType.Emoji;
                case "GRADIENT_ANGULAR":
                    return FillType.GradientAngular;
                case "GRADIENT_DIAMOND":
                    return FillType.GradientDiamond;
                case "GRADIENT_LINEAR":
                    return FillType.GradientLinear;
                case "GRADIENT_RADIAL":
                    return FillType.GradientRadial;
                case "IMAGE":
                    return FillType.Image;
                case "SOLID":
                    return FillType.Solid;
            }
            throw new Exception("Cannot unmarshal type FillType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FillType)untypedValue;
            switch (value)
            {
                case FillType.Emoji:
                    serializer.Serialize(writer, "EMOJI");
                    return;
                case FillType.GradientAngular:
                    serializer.Serialize(writer, "GRADIENT_ANGULAR");
                    return;
                case FillType.GradientDiamond:
                    serializer.Serialize(writer, "GRADIENT_DIAMOND");
                    return;
                case FillType.GradientLinear:
                    serializer.Serialize(writer, "GRADIENT_LINEAR");
                    return;
                case FillType.GradientRadial:
                    serializer.Serialize(writer, "GRADIENT_RADIAL");
                    return;
                case FillType.Image:
                    serializer.Serialize(writer, "IMAGE");
                    return;
                case FillType.Solid:
                    serializer.Serialize(writer, "SOLID");
                    return;
            }
            throw new Exception("Cannot marshal type FillType");
        }

        public static readonly FillTypeConverter Singleton = new FillTypeConverter();
    }

    internal class AlignmentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Alignment) || t == typeof(Alignment?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CENTER":
                    return Alignment.Center;
                case "MAX":
                    return Alignment.Max;
                case "MIN":
                    return Alignment.Min;
            }
            throw new Exception("Cannot unmarshal type Alignment");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Alignment)untypedValue;
            switch (value)
            {
                case Alignment.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case Alignment.Max:
                    serializer.Serialize(writer, "MAX");
                    return;
                case Alignment.Min:
                    serializer.Serialize(writer, "MIN");
                    return;
            }
            throw new Exception("Cannot marshal type Alignment");
        }

        public static readonly AlignmentConverter Singleton = new AlignmentConverter();
    }

    internal class PatternConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Pattern) || t == typeof(Pattern?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "COLUMNS":
                    return Pattern.Columns;
                case "GRID":
                    return Pattern.Grid;
                case "ROWS":
                    return Pattern.Rows;
            }
            throw new Exception("Cannot unmarshal type Pattern");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Pattern)untypedValue;
            switch (value)
            {
                case Pattern.Columns:
                    serializer.Serialize(writer, "COLUMNS");
                    return;
                case Pattern.Grid:
                    serializer.Serialize(writer, "GRID");
                    return;
                case Pattern.Rows:
                    serializer.Serialize(writer, "ROWS");
                    return;
            }
            throw new Exception("Cannot marshal type Pattern");
        }

        public static readonly PatternConverter Singleton = new PatternConverter();
    }

    internal class StrokeAlignConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(StrokeAlign) || t == typeof(StrokeAlign?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CENTER":
                    return StrokeAlign.Center;
                case "INSIDE":
                    return StrokeAlign.Inside;
                case "OUTSIDE":
                    return StrokeAlign.Outside;
            }
            throw new Exception("Cannot unmarshal type StrokeAlign");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StrokeAlign)untypedValue;
            switch (value)
            {
                case StrokeAlign.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case StrokeAlign.Inside:
                    serializer.Serialize(writer, "INSIDE");
                    return;
                case StrokeAlign.Outside:
                    serializer.Serialize(writer, "OUTSIDE");
                    return;
            }
            throw new Exception("Cannot marshal type StrokeAlign");
        }

        public static readonly StrokeAlignConverter Singleton = new StrokeAlignConverter();
    }

    internal class TextAlignHorizontalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TextAlignHorizontal) || t == typeof(TextAlignHorizontal?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CENTER":
                    return TextAlignHorizontal.Center;
                case "JUSTIFIED":
                    return TextAlignHorizontal.Justified;
                case "LEFT":
                    return TextAlignHorizontal.Left;
                case "RIGHT":
                    return TextAlignHorizontal.Right;
            }
            throw new Exception("Cannot unmarshal type TextAlignHorizontal");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TextAlignHorizontal)untypedValue;
            switch (value)
            {
                case TextAlignHorizontal.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case TextAlignHorizontal.Justified:
                    serializer.Serialize(writer, "JUSTIFIED");
                    return;
                case TextAlignHorizontal.Left:
                    serializer.Serialize(writer, "LEFT");
                    return;
                case TextAlignHorizontal.Right:
                    serializer.Serialize(writer, "RIGHT");
                    return;
            }
            throw new Exception("Cannot marshal type TextAlignHorizontal");
        }

        public static readonly TextAlignHorizontalConverter Singleton = new TextAlignHorizontalConverter();
    }

    internal class TextAlignVerticalConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TextAlignVertical) || t == typeof(TextAlignVertical?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BOTTOM":
                    return TextAlignVertical.Bottom;
                case "CENTER":
                    return TextAlignVertical.Center;
                case "TOP":
                    return TextAlignVertical.Top;
            }
            throw new Exception("Cannot unmarshal type TextAlignVertical");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TextAlignVertical)untypedValue;
            switch (value)
            {
                case TextAlignVertical.Bottom:
                    serializer.Serialize(writer, "BOTTOM");
                    return;
                case TextAlignVertical.Center:
                    serializer.Serialize(writer, "CENTER");
                    return;
                case TextAlignVertical.Top:
                    serializer.Serialize(writer, "TOP");
                    return;
            }
            throw new Exception("Cannot marshal type TextAlignVertical");
        }

        public static readonly TextAlignVerticalConverter Singleton = new TextAlignVerticalConverter();
    }

    internal class NodeTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NodeType) || t == typeof(NodeType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BOOLEAN":
                    return NodeType.Boolean;
                case "CANVAS":
                    return NodeType.Canvas;
                case "COMPONENT":
                    return NodeType.Component;
                case "DOCUMENT":
                    return NodeType.Document;
                case "ELLIPSE":
                    return NodeType.Ellipse;
                case "FRAME":
                    return NodeType.Frame;
                case "GROUP":
                    return NodeType.Group;
                case "INSTANCE":
                    return NodeType.Instance;
                case "LINE":
                    return NodeType.Line;
                case "RECTANGLE":
                    return NodeType.Rectangle;
                case "REGULAR_POLYGON":
                    return NodeType.RegularPolygon;
                case "SLICE":
                    return NodeType.Slice;
                case "STAR":
                    return NodeType.Star;
                case "TEXT":
                    return NodeType.Text;
                case "VECTOR":
                    return NodeType.Vector;
            }
            throw new Exception("Cannot unmarshal type NodeType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NodeType)untypedValue;
            switch (value)
            {
                case NodeType.Boolean:
                    serializer.Serialize(writer, "BOOLEAN");
                    return;
                case NodeType.Canvas:
                    serializer.Serialize(writer, "CANVAS");
                    return;
                case NodeType.Component:
                    serializer.Serialize(writer, "COMPONENT");
                    return;
                case NodeType.Document:
                    serializer.Serialize(writer, "DOCUMENT");
                    return;
                case NodeType.Ellipse:
                    serializer.Serialize(writer, "ELLIPSE");
                    return;
                case NodeType.Frame:
                    serializer.Serialize(writer, "FRAME");
                    return;
                case NodeType.Group:
                    serializer.Serialize(writer, "GROUP");
                    return;
                case NodeType.Instance:
                    serializer.Serialize(writer, "INSTANCE");
                    return;
                case NodeType.Line:
                    serializer.Serialize(writer, "LINE");
                    return;
                case NodeType.Rectangle:
                    serializer.Serialize(writer, "RECTANGLE");
                    return;
                case NodeType.RegularPolygon:
                    serializer.Serialize(writer, "REGULAR_POLYGON");
                    return;
                case NodeType.Slice:
                    serializer.Serialize(writer, "SLICE");
                    return;
                case NodeType.Star:
                    serializer.Serialize(writer, "STAR");
                    return;
                case NodeType.Text:
                    serializer.Serialize(writer, "TEXT");
                    return;
                case NodeType.Vector:
                    serializer.Serialize(writer, "VECTOR");
                    return;
            }
            throw new Exception("Cannot marshal type NodeType");
        }

        public static readonly NodeTypeConverter Singleton = new NodeTypeConverter();
    }
}
