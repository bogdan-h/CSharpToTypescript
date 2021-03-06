﻿/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class ArrayRankSpecifierTranslation : CSharpSyntaxTranslation
    {
        public new ArrayRankSpecifierSyntax Syntax
        {
            get { return (ArrayRankSpecifierSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ArrayRankSpecifierTranslation() { }
        public ArrayRankSpecifierTranslation(ArrayRankSpecifierSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {

            Sizes = syntax.Sizes.Get<ExpressionSyntax, ExpressionTranslation>( this );
        }

        public SeparatedSyntaxListTranslation<ExpressionSyntax, ExpressionTranslation> Sizes { get; set; }

        protected override string InnerTranslate()
        {
            return $"[{Sizes.Translate()}]";
        }
    }
}
