module Vector
open Aardvark.Base.Incremental
[<DomainType>]  
type State ={
    x :Numeric.State
    y :Numeric.State
    z :Numeric.State
}
type Msg =
|UpdateX of Numeric.Msg
|UpdateY of Numeric.Msg
|UpdateZ of Numeric.Msg

let update (m) (msg) = 
    match msg with
    |UpdateX v -> {m with x = Numeric.update m.x v}
    |UpdateY v -> {m with y = Numeric.update m.y v}
    |UpdateZ v -> {m with z = Numeric.update m.z v}

