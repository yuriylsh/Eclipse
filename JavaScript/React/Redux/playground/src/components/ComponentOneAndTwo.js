import Child from './Child';
import {connect} from 'react-redux';

export const ComponentOne = connect(
    (state) => ({ textToDisplay: state.branchOne })
)(Child);


export const ComponentTwo = connect(
    (state) => ({ textToDisplay: state.branchTwo })
)(Child);