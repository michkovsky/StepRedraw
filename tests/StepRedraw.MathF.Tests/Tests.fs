module Tests

open System
open Xunit
open MathNet.Numerics.LinearAlgebra

[<Fact>]
let ``My test`` () =
    //define test vectors
    let a = vector[1.0; 1.0]
    let b = vector[6.0; 5.0]
    let c = vector[5.0; 2.0]

    Assert.True(true)
