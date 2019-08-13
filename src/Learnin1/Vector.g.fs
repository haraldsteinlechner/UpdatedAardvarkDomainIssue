namespace Vector

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Vector

[<AutoOpen>]
module Mutable =

    
    
    type MState(__initial : Vector.State) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Vector.State> = Aardvark.Base.Incremental.EqModRef<Vector.State>(__initial) :> Aardvark.Base.Incremental.IModRef<Vector.State>
        let _x = Numeric.Mutable.MState.Create(__initial.x)
        let _y = Numeric.Mutable.MState.Create(__initial.y)
        let _z = Numeric.Mutable.MState.Create(__initial.z)
        
        member x.x = _x
        member x.y = _y
        member x.z = _z
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Vector.State) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                Numeric.Mutable.MState.Update(_x, v.x)
                Numeric.Mutable.MState.Update(_y, v.y)
                Numeric.Mutable.MState.Update(_z, v.z)
                
        
        static member Create(__initial : Vector.State) : MState = MState(__initial)
        static member Update(m : MState, v : Vector.State) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Vector.State> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module State =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let x =
                { new Lens<Vector.State, Numeric.State>() with
                    override x.Get(r) = r.x
                    override x.Set(r,v) = { r with x = v }
                    override x.Update(r,f) = { r with x = f r.x }
                }
            let y =
                { new Lens<Vector.State, Numeric.State>() with
                    override x.Get(r) = r.y
                    override x.Set(r,v) = { r with y = v }
                    override x.Update(r,f) = { r with y = f r.y }
                }
            let z =
                { new Lens<Vector.State, Numeric.State>() with
                    override x.Get(r) = r.z
                    override x.Set(r,v) = { r with z = v }
                    override x.Update(r,f) = { r with z = f r.z }
                }
