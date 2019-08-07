namespace Components
open Aardvark.Base.Incremental
module ModNumeric =
    [<DomainType>]
    type Model ={
        value:float
    }
    type Msg = 
    |Increment
    |Decrement

    let update m msg=             
        match msg with 
        | Increment -> {m with value = m.value + 0.1}
        | Decrement -> {m with value = m.value - 0.1}



module ModVector =
    [<DomainType>]
    type VectorModel ={
        x :ModNumeric.Model
        y :ModNumeric.Model
        z :ModNumeric.Model
    }
    type Msg =
    |UpdateX of ModNumeric.Msg
    |UpdateY of ModNumeric.Msg
    |UpdateZ of ModNumeric.Msg

    let update (m) (msg) = 
        match msg with
        |UpdateX v -> {m with x = ModNumeric.update m.x v}
        |UpdateY v -> {m with y = ModNumeric.update m.y v}
        |UpdateZ v -> {m with z = ModNumeric.update m.z v}

