module Numeric

open System
open Aardvark.Base.Incremental



type MState(__initial : State) =
    inherit obj()
    let mutable __current : Aardvark.Base.Incremental.IModRef<State> = Aardvark.Base.Incremental.EqModRef<State>(__initial) :> Aardvark.Base.Incremental.IModRef<State>
    let _value = ResetMod.Create(__initial.value)
    
    member x.value = _value :> IMod<_>
    
    member x.Current = __current :> IMod<_>
    member x.Update(v : State) =
        if not (System.Object.ReferenceEquals(__current.Value, v)) then
            __current.Value <- v
            
            ResetMod.Update(_value,v.value)
            
    
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
        let value =
            { new Lens<State, System.Double>() with
                override x.Get(r) = r.value
                override x.Set(r,v) = { r with value = v }
                override x.Update(r,f) = { r with value = f r.value }
            }
