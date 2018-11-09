import React, { Component, SFC } from 'react'
import { loadSubscriptions, ISubscription, submitSubscriptions, ISubscriptionUpdate } from '../api'
import './notificationManager.scss'

interface INotificationManagerState {
    subscriptions: ISubscription[]
    isDirty: boolean
}

interface INotificationManagerProps {
    userId: number
    userToken: string
}

export class NotificationManager extends Component<INotificationManagerProps, INotificationManagerState> {

    state: INotificationManagerState = {
        subscriptions: [],
        isDirty: false
    }

    constructor(props: INotificationManagerProps){
        super(props)
        this.handleSubscriptionChange = this.handleSubscriptionChange.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
    }

    componentDidMount() {
        this.loadCurrentSubscriptions();
    }

    render() {
        return (
            <>
                <h2>Notification Management</h2>                
                <div className="notificationManagerContainer">
                    {this.state.subscriptions.map(s => 
                        <Subscription key={s.id} subscription={s} handleSubscriptionChange={this.handleSubscriptionChange} />)}
                </div>
                <input onClick={this.handleSubmit} type="button" className="updateButton" value="Update" disabled={!this.state.isDirty}/>                
            </>
        )
    }

    loadCurrentSubscriptions() {
        const { userId, userToken } = this.props
        loadSubscriptions(userId, userToken).then(response => this.setState({
            subscriptions: response
        }))
    }

    handleSubmit(event: React.MouseEvent<HTMLInputElement>){
        const {userId, userToken} = this.props        
        this.setState({isDirty: false})
        const updates: ISubscriptionUpdate[] = this.state.subscriptions.map(
            x => ({id: x.id, isUnsubscribed: x.isUnsubscribed}) )
        submitSubscriptions(userId, updates, userToken)
    }

    handleSubscriptionChange(event: React.ChangeEvent<HTMLInputElement>){
        const {subscriptions: subs} = this.state
        const id = parseInt(event.target.value)
        const changedIndex = subs.findIndex(x => x.id == id)
        const changed = subs[changedIndex]
        const subscriptions: ISubscription[] = [
            ...subs.slice(0, changedIndex),
            {...changed, isUnsubscribed: !changed.isUnsubscribed},
            ...subs.slice(changedIndex + 1)
        ]
        this.setState({subscriptions, isDirty: true})
    }
}

interface SubscriptionProps {
    subscription: ISubscription,
    handleSubscriptionChange(event: React.ChangeEvent<HTMLInputElement>): void
}

const Subscription: SFC<SubscriptionProps> = ({ subscription, handleSubscriptionChange }) => {
    const { id, isUnsubscribed, name } = subscription
    const idString = id.toString()
    return <>
        <span>{name}</span><input id={idString} name={idString} type="checkbox" value={id} checked={isUnsubscribed} onChange={handleSubscriptionChange}/>
    </>
}
