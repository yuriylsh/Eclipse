import React, { Component } from 'react'
import { Login } from './Login'
import { authenticate } from '../api'
import {NotificationManager} from './NotificationManager'

interface RootState {
    isAuthenticating: boolean,
    userId?: number,
    userToken?: string
}

export class Root extends Component<{}, RootState> {
    state: RootState = {
        isAuthenticating: false
    }

    componentDidMount() {
        this.performAuthentication();
    }

    render() {
        const { isAuthenticating, userId, userToken } = this.state
        const showUserData = userId && !isAuthenticating
        return (
            <>
                <Login
                    isAuthenticating={isAuthenticating}
                    userId={userId}
                    userToken={userToken}
                    swithToUser={user => this.performAuthentication(user.id)}>
                </Login>
                {showUserData && <NotificationManager userId={userId!} userToken={userToken!} />}
                <footer>Yuriy Lyeshchenko © 2018</footer>
            </>
        )
    }

    performAuthentication(id?: number) {
        this.setState({ isAuthenticating: true })
        authenticate(id).then(authResponse => this.setState({
            isAuthenticating: false,
            userId: authResponse.id,
            userToken: authResponse.token
        }))
    }
}
