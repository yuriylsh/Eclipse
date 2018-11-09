export interface IAuthenticationResponse {
    id: number
    token: string
}

export const authenticate = (id?: number): Promise<IAuthenticationResponse> => {
    var body = new FormData();
    if (id) {
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

export interface ISubscription {
    id: number
    name: string
    isUnsubscribed: boolean
}

export const loadSubscriptions = (userId: number, jwtToken: string): Promise<ISubscription[]> =>
    fetch(`/api/users/${userId}/subscriptions`, addAuthentication({
        method: 'GET',
        cache: 'no-cache'
    }, jwtToken)).then(async response => (await response.json()) as ISubscription[])

export interface ISubscriptionUpdate {
    id: number,
    isUnsubscribed: boolean
}

export const submitSubscriptions = async (userId: number, updates: ISubscriptionUpdate[], jwtToken: string) => {
    var body = new FormData();    
    body.append('updates', JSON.stringify(updates))
    const response = await fetch(`/api/users/${userId}/subscriptions`, addAuthentication({
        method: 'PUT',
        body:  JSON.stringify(updates)
    }, jwtToken, ["Content-Type", "application/json"]));
    return response.status;
}


const addAuthentication = (request: RequestInit, jwtToken: string, extraHeader?: string[]) => {
    request.headers = [
        ['Authorization', 'Bearer ' + jwtToken]
    ]
    if(extraHeader) request.headers.push(extraHeader)
    return request;
}