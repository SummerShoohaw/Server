five methods in controller

1>  http url: ./api/game/createuser/{username?}
    purpose: create new user
    parameter: username --> string

2>  http url: ./api/game/haspet/{username?}
    purpose: check user has pet or not
    return value: 0 --> doesnt have pet
                  1 --> has pet

3>  http url: ./api/game/getpet/{username?}
    purpose:get pet (when user has pet)
    parameter: username --> string
    return value: Pet Object (refer to file: Models.Pet)

4>  http url: ./api/game/createpet/{username?} 
    purpose: create new pet (when user doesnt have a pet)
    parameter: username --> string
    querystring: urlnum --> int (1 ~ 4, to represent the pictures)
                 petname --> string
                
5>  http url: ./api/game/update/{username?}
    purpose: to update infomation of a pet
    paramter: username --> string
    querystring: content --> int (use number to represent what is going to updated, 1->Age, 2->Weight, 3->Exercise)
                 number --> int (values going to be added to the original value)


TODO list (server-side):

 1> specify the urls, Models/DBConnector.cs line-9