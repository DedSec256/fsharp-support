using JetBrains.ReSharper.Plugins.FSharp.Psi.Tree;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Plugins.FSharp.Psi.Impl.Tree
{
  internal partial class ObjExpr
  {
    public override IFSharpIdentifierLikeNode NameIdentifier => TypeName.Identifier;

    protected override string DeclaredElementName =>
      GetSourceFile() is IPsiSourceFile sourceFile && sourceFile.GetLocation() is var path && !path.IsEmpty
        ? "Object expression in " + path.Name + "@" + GetTreeStartOffset()
        : SharedImplUtil.MISSING_DECLARATION_NAME;

    public bool IsConstantValue() => false;
    public ConstantValue ConstantValue => ConstantValue.BAD_VALUE;
    public ExpressionAccessType GetAccessType() => ExpressionAccessType.None;

    // todo: use type from reference name
    public IType Type() => TypeFactory.CreateUnknownType(GetPsiModule());
    public IExpressionType GetExpressionType() => Type();
    public IType GetImplicitlyConvertedTo() => Type();
  }
}
