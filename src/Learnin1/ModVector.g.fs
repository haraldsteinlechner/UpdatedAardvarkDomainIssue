namespace Components

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Components

[<AutoOpen>]
module Mutable =

    module ModNumeric =
        open ModNumeric
        
        
        
        type MModel(__initial : Components.ModNumeric.Model) =
            inherit obj()
            let mutable __current : Aardvark.Base.Incremental.IModRef<Components.ModNumeric.Model> = Aardvark.Base.Incremental.EqModRef<Components.ModNumeric.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Components.ModNumeric.Model>
            let _value = ResetMod.Create(__initial.value)
            
            member x.value = _value :> IMod<_>
            
            member x.Current = __current :> IMod<_>
            member x.Update(v : Components.ModNumeric.Model) =
                if not (System.Object.ReferenceEquals(__current.Value, v)) then
                    __current.Value <- v
                    
                    ResetMod.Update(_value,v.value)
                    
            
            static member Create(__initial : Components.ModNumeric.Model) : MModel = MModel(__initial)
            static member Update(m : MModel, v : Components.ModNumeric.Model) = m.Update(v)
            
            override x.ToString() = __current.Value.ToString()
            member x.AsString = sprintf "%A" __current.Value
            interface IUpdatable<Components.ModNumeric.Model> with
                member x.Update v = x.Update v
        
        
        
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Model =
            [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
            module Lens =
                let value =
                    { new Lens<Components.ModNumeric.Model, System.Double>() with
                        override x.Get(r) = r.value
                        override x.Set(r,v) = { r with value = v }
                        override x.Update(r,f) = { r with value = f r.value }
                    }
    module ModVector =
        open ModVector
        
        
        
        type MVectorModel(__initial : Components.ModVector.VectorModel) =
            inherit obj()
            let mutable __current : Aardvark.Base.Incremental.IModRef<Components.ModVector.VectorModel> = Aardvark.Base.Incremental.EqModRef<Components.ModVector.VectorModel>(__initial) :> Aardvark.Base.Incremental.IModRef<Components.ModVector.VectorModel>
            let _x = Mutable.Components.ModNumeric.MModel.Create(__initial.x)
            let _y = Mutable.Components.ModNumeric.MModel.Create(__initial.y)
            let _z = Mutable.Components.ModNumeric.MModel.Create(__initial.z)
            
            member x.x = _x
            member x.y = _y
            member x.z = _z
            
            member x.Current = __current :> IMod<_>
            member x.Update(v : Components.ModVector.VectorModel) =
                if not (System.Object.ReferenceEquals(__current.Value, v)) then
                    __current.Value <- v
                    
                    Mutable.Components.ModNumeric.MModel.Update(_x, v.x)
                    Mutable.Components.ModNumeric.MModel.Update(_y, v.y)
                    Mutable.Components.ModNumeric.MModel.Update(_z, v.z)
                    
            
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
