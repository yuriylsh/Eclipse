export interface IAuthenticationResponse {
    id: number
    token: string
}

export const authenticate = (id?: number): Promise<IAuthenticationResponse> => {
    var body = new FormData();    
    if(id){
        body.append('id', id.toString())
    }
    return fetch("/api/authentication/login", {
        method: 'POST',
        body
    }).then(async response => (await response.json()) as IAuthenticationResponse)
}

export interface IUser {
    id: number
    firstName: string
    lastName: string
}

export const getAllUsers = (jwtToken: string): Promise<IUser[]> =>
    fetch("/api/users", addAuthentication({
        method: 'GET',
        cache: 'no-cache'
    }, jwtToken)).then(async response => (await response.json()) as IUser[])

export interface ISubscription{
    id: number
    name: string
    isUnsubscribed: boolean
}

export const loadSubscriptions = (userId: number, jwtToken: string): Promise<ISubscription[]> =>
    fetch(`/api/users/${userId}/subscriptions`, addAuthentication({
        method: 'GET',
        cache: 'no-cache'
    }, jwtToken)).then(async response => (await response.json()) as ISubscription[])


const addAuthentication = (request: RequestInit, jwtToken: string) => {
    request.headers = [
        ['Authorization', 'Bearer ' + jwtToken]
    ]
    return request;
}