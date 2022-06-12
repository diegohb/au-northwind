//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


namespace SharedKernel.Specs;

using System.Linq.Expressions;
using Expressions;

/// <summary>
///   A Logic OR Specification
/// </summary>
/// <typeparam name="T">Type of entity that check this specification</typeparam>
public sealed class OrSpecification<T>
  : CompositeSpecification<T>
  where T : class
{
  #region Public Constructor

  /// <summary>
  ///   Default constructor for AndSpecification
  /// </summary>
  /// <param name="leftSide">Left side specification</param>
  /// <param name="rightSide">Right side specification</param>
  public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
  {
    if (leftSide == null)
    {
      throw new ArgumentNullException("leftSide");
    }

    if (rightSide == null)
    {
      throw new ArgumentNullException("rightSide");
    }

    LeftSideSpecification = leftSide;
    RightSideSpecification = rightSide;
  }

  #endregion

  #region Members

  #endregion

  #region Composite Specification overrides

  /// <summary>
  ///   Left side specification
  /// </summary>
  public override ISpecification<T> LeftSideSpecification { get; }

  /// <summary>
  ///   Righ side specification
  /// </summary>
  public override ISpecification<T> RightSideSpecification { get; }

  /// <summary>
  ///   <see cref="ISpecification{TEntity}" />
  /// </summary>
  /// <returns>
  ///   <see cref="ISpecification{TEntity}" />
  /// </returns>
  public override Expression<Func<T, bool>> SatisfiedBy()
  {
    var left = LeftSideSpecification.SatisfiedBy();
    var right = RightSideSpecification.SatisfiedBy();

    return left.Or(right);
  }

  #endregion
}