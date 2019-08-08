module Vector

open System
open Aardvark.Base.Incremental



type MState(__initial : State) =
    inherit obj()
    let mutable __current : Aardvark.Base.Incremental.IModRef<State> = Aardvark.Base.Incremental.EqModRef<State>(__initial) :> Aardvark.Base.Incremental.IModRef<State>
    let _x = Mutable.Numeric.MState.Create(__initial.x)
    let _y = Mutable.Numeric.MState.Create(__initial.y)
    let _z = Mutable.Numeric.MState.Create(__initial.z)
    
    member x.x = _x
    member x.y = _y
    member x.z = _z
    
    member x.Current = __current :> IMod<_>
    member x.Update(v : State) =
        if not (System.Object.ReferenceEquals(__current.Value, v)) then
            __current.Value <- v
            
            Mutable.Numeric.MState.Update(_x, v.x)
            Mutable.Numeric.MState.Update(_y, v.y)
            Mutable.Numeric.MState.Update(_z, v.z)
            
    
    static member Create(__initial : State) : MState = MState(__initial)
    static member Update(m : MState, v : State) = m.Update(v)
    
    override x.ToString() = __current.Value.ToString()
    member x.AsString = sprintf "%A" __current.Value
    interface IUpdatable<State> with
        member x.Update v = x.Update v



[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module State =
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Lens =
        let x =
            { new Lens<State, Numeric.State>() with
                override x.Get(r) = r.x
                override x.Set(r,v) = { r with x = v }
                override x.Update(r,f) = { r with x = f r.x }
            }
        let y =
            { new Lens<State, Numeric.State>() with
                override x.Get(r) = r.y
                override x.Set(r,v) = { r with y = v }
                override x.Update(r,f) = { r with y = f r.y }
            }
        let z =
            { new Lens<State, Numeric.State>() with
                override x.Get(r) = r.z
                override x.Set(r,v) = { r with z = v }
                override x.Update(r,f) = { r with z = f r.z }
            }
