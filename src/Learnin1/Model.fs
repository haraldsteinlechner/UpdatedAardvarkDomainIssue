namespace Learnin1.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI.Primitives
open Components

type Primitive =
    | Box
    | Sphere
    


[<DomainType>]
type Model =
    {
        currentModel    : Primitive
        cameraState     : CameraControllerState
        vec : Components.ModVector.VectorModel
    }