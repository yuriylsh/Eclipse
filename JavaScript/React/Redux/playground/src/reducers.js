import { combineReducers } from 'redux';

let oneCounter = 0;
const branchOne = (state = "", action) => {
    if (action.type === "ONE") {
        return "one" + String(oneCounter++);
    } else {
        return state;
    }
}

let twoCounter = 0;
const branchTwo = (state = "", action) => {
    if (action.type === "TWO") {
        return "two" + String(twoCounter++);
    } else {
        return state;
    }
}

const appReducer = combineReducers({ branchOne, branchTwo });
export default appReducer;