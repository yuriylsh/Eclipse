import React, {Component} from 'react';
import Button from './Button';
import {ComponentOne, ComponentTwo} from './ComponentOneAndTwo';

class Root extends Component{
    render(){
        const {oneClicked, twoClicked} = this.props;
        return (
            <div>
                <Button onClick={oneClicked} name="One" />
                <Button onClick={twoClicked} name="Two" />
                <div>
                    <ComponentOne />
                    <ComponentTwo />
                </div>
            </div>);
    }

    shouldComponentUpdate(){
        return false;
    }
}

export default Root;