namespace Learnin1.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Learnin1.Model

[<AutoOpen>]
module Mutable =

    
    
    type MModel(__initial : Learnin1.Model.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Learnin1.Model.Model> = Aardvark.Base.Incremental.EqModRef<Learnin1.Model.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Learnin1.Model.Model>
        let _currentModel = ResetMod.Create(__initial.currentModel)
        let _cameraState = Aardvark.UI.Primitives.Mutable.MCameraControllerState.Create(__initial.cameraState)
        let _vectorState = Mutable.Vector.MState.Create(__initial.vectorState)
        
        member x.currentModel = _currentModel :> IMod<_>
        member x.cameraState = _cameraState
        member x.vectorState = _vectorState
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Learnin1.Model.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_currentModel,v.currentModel)
                Aardvark.UI.Primitives.Mutable.MCameraControllerState.Update(_cameraState, v.cameraState)
                Mutable.Vector.MState.Update(_vectorState, v.vectorState)
                
        
        static member Create(__initial : Learnin1.Model.Model) : MModel = MModel(__initial)
        static member Update(m : MModel, v : Learnin1.Model.Model) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Learnin1.Model.Model> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Model =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let currentModel =
                { new Lens<Learnin1.Model.Model, Learnin1.Model.Primitive>() with
                    override x.Get(r) = r.currentModel
                    override x.Set(r,v) = { r with currentModel = v }
                    override x.Update(r,f) = { r with currentModel = f r.currentModel }
                }
            let cameraState =
                { new Lens<Learnin1.Model.Model, Aardvark.UI.Primitives.CameraControllerState>() with
                    override x.Get(r) = r.cameraState
                    override x.Set(r,v) = { r with cameraState = v }
                    override x.Update(r,f) = { r with cameraState = f r.cameraState }
                }
            let vectorState =
                { new Lens<Learnin1.Model.Model, Vector.State>() with
                    override x.Get(r) = r.vectorState
                    override x.Set(r,v) = { r with vectorState = v }
                    override x.Update(r,f) = { r with vectorState = f r.vectorState }
                }
