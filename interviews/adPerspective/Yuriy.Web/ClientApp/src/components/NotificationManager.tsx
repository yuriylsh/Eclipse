import React, { Component, SFC } from 'react'
import { loadSubscriptions, ISubscription } from '../api'
import './notificationManager.scss'

interface INotificationManagerState {
    subscriptions: ISubscription[]
}

interface INotificationManagerProps {
    userId: number
    userToken: string
}

export class NotificationManager extends Component<INotificationManagerProps, INotificationManagerState> {

    state: INotificationManagerState = {
        subscriptions: []
    }

    componentDidMount() {
        this.loadCurrentSubscriptions();
    }

    render() {
        return (
            <>
                <h2>Notification Management</h2>
                <div className="notificationManagerContainer">
                    {this.state.subscriptions.map(s => <Subscription subscription={s} />)}
                </div>
            </>
        )
    }

    loadCurrentSubscriptions() {
        const { userId, userToken } = this.props
        loadSubscriptions(userId, userToken).then(response => this.setState({
            subscriptions: response
        }))
    }
}

interface SubscriptionProps {
    subscription: ISubscription
}

const Subscription: SFC<SubscriptionProps> = ({ subscription }) =>
    <>
        {subscription.name}<input type="checkbox" checked={subscription.isUnsubscribed}></input>
    </>
