namespace Components

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Components

[<AutoOpen>]
module Mutable =

    module ModVector =
        open ModVector
        
        
        
        type MVectorModel(__initial : Components.ModVector.VectorModel) =
            inherit obj()
            let mutable __current : Aardvark.Base.Incremental.IModRef<Components.ModVector.VectorModel> = Aardvark.Base.Incremental.EqModRef<Components.ModVector.VectorModel>(__initial) :> Aardvark.Base.Incremental.IModRef<Components.ModVector.VectorModel>
            let _x = ResetMod.Create(__initial.x)
            let _y = ResetMod.Create(__initial.y)
            let _z = ResetMod.Create(__initial.z)
            
            member x.x = _x :> IMod<_>
            member x.y = _y :> IMod<_>
            member x.z = _z :> IMod<_>
            
            member x.Current = __current :> IMod<_>
            member x.Update(v : Components.ModVector.VectorModel) =
                if not (System.Object.ReferenceEquals(__current.Value, v)) then
                    __current.Value <- v
                    
                    ResetMod.Update(_x,v.x)
                    ResetMod.Update(_y,v.y)
                    ResetMod.Update(_z,v.z)
                    
            
            static member Create(__initial : Components.ModVector.VectorModel) : MVectorModel = MVectorModel(__initial)
            static member Update(m : MVectorModel, v : Components.ModVector.VectorModel) = m.Update(v)
            
            override x.ToString() = __current.Value.ToString()
            member x.AsString = sprintf "%A" __current.Value
            interface IUpdatable<Components.ModVector.VectorModel> with
                member x.Update v = x.Update v
        
        
        
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module VectorModel =
            [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
            module Lens =
                let x =
                    { new Lens<Components.ModVector.VectorModel, Components.ModNumeric.Model>() with
                        override x.Get(r) = r.x
                        override x.Set(r,v) = { r with x = v }
                        override x.Update(r,f) = { r with x = f r.x }
                    }
                let y =
                    { new Lens<Components.ModVector.VectorModel, Components.ModNumeric.Model>() with
                        override x.Get(r) = r.y
                        override x.Set(r,v) = { r with y = v }
                        override x.Update(r,f) = { r with y = f r.y }
                    }
                let z =
                    { new Lens<Components.ModVector.VectorModel, Components.ModNumeric.Model>() with
                        override x.Get(r) = r.z
                        override x.Set(r,v) = { r with z = v }
                        override x.Update(r,f) = { r with z = f r.z }
                    }
