import React, { Component, SFC } from 'react'
import { IUser, getAllUsers } from '../api'
import './login.scss'

interface ILoginState {
    users: IUser[]
}

interface ILoginProps {
    isAuthenticating: boolean
    userId?: number
    userToken?: string
    swithToUser(user: IUser): void
}

export class Login extends Component<ILoginProps, ILoginState> {
    state: ILoginState = {
        users: []
    }

    componentDidUpdate(prevProps: ILoginProps) {
        const { userToken: currentUserToken } = this.props
        if (!prevProps.userToken && currentUserToken) {
            getAllUsers(currentUserToken).then(users => this.setState({ users }))
        }
    }

    render() {
        const { swithToUser } = this.props
        const currentUser: (IUser | undefined) = this.getCurrentUser()
        const switchToUsers: IUser[] = this.getSwitchToUsers()
        return (
            <div className="login">
                {currentUser && <Welcome user={currentUser!} />}
                {switchToUsers.length && <SwitchTo users={switchToUsers} swithToUser={swithToUser} />}
            </div>
        )
    }

    getCurrentUser() {
        const { isAuthenticating, userId } = this.props
        const { users } = this.state
        if (!isAuthenticating && userId && users.length) {
            return users.find(user => user.id === userId)
        }
    }

    getSwitchToUsers(): IUser[] {
        const currentUser = this.getCurrentUser()
        return currentUser ? this.state.users.filter(user => user.id !== currentUser.id) : []
    }
}

const Welcome: SFC<{ user: IUser }> = ({ user }) =>
    <div className="welcomeContainer">
        Welcome <span>{user.firstName} {user.lastName}</span>
    </div>

const SwitchTo: SFC<{ users: IUser[], swithToUser: (user: IUser) => void }> = ({ users, swithToUser }) =>
    <div className="switchtoContainer">
        Switch to: {users.map(user => <span key={user.id} onClick={(evt) => {evt.preventDefault; swithToUser(user);}}>{user.firstName} {user.lastName}</span>)}
    </div>
