namespace Learnin1

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Learnin1.Model

open Vector

type Message =
    | ToggleModel
    | CameraMessage of FreeFlyController.Message
    | VUpdate of Vector.Msg

module App =
    
    let initial = { currentModel = Box; cameraState = FreeFlyController.initial ; vectorState = {x = {value = 1.0}; y= {value =2.0}; z = {value = 2.0}} }

    let update (m : Model) (msg : Message) =
        match msg with
            | ToggleModel -> 
                match m.currentModel with
                    | Box -> { m with currentModel = Sphere }
                    | Sphere -> { m with currentModel = Box }

            | CameraMessage msg ->
                { m with cameraState = FreeFlyController.update m.cameraState msg }
            |VUpdate msg -> {m with vectorState = Vector.update m.vectorState msg }

    let view (m : MModel) =

        let frustum = 
            Frustum.perspective 60.0 0.1 100.0 1.0 
                |> Mod.constant

        let sg =
            m.currentModel |> Mod.map (fun v ->
                match v with
                    | Box -> Sg.box (Mod.constant C4b.Red) (Mod.constant (Box3d(-V3d.III, V3d.III)))
                    | Sphere -> Sg.sphere 5 (Mod.constant C4b.Green) (Mod.constant 1.0)
            )
            |> Sg.dynamic
            |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.simpleLighting
            }

        let att =
            [
                style "position: fixed; left: 0; top: 0; width: 100%; height: 100%"
            ]
        require Html.semui (
            body [] [
                FreeFlyController.controlledControl m.cameraState CameraMessage frustum (AttributeMap.ofList att) sg

                div [style "position: fixed; left: 20px; top: 20px;color:white;";] [
                    //button [clazz "ui button";onClick (fun _ -> Increment)] [text "+"]
                    //button [clazz "ui button";onClick (fun _ -> Increment)] [text "-"]
                    br[]
                    text "My Value:"
                    //Incremental.text (m.value |> Mod.map(fun x -> sprintf "%.1f" x))
                    br[]
                    br[]
                    button [onClick (fun _ -> ToggleModel)] [text "Toggle Model"]
                ]

            ])

    let app =
        {
            initial = initial
            update = update
            view = view
            threads = Model.Lens.cameraState.Get >> FreeFlyController.threads >> ThreadPool.map CameraMessage
            unpersist = Unpersist.instance
        }