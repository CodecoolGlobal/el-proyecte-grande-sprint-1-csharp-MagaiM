import React, {useCallback} from 'react';
import {sortByList} from '../utils/Sortby';

const SortByCheckboxes = ({handleOnChange, set, isChecked, List, name}) => {
  return (
    <ul className='checkbox-div'>
    <div className='checkbox-div-title'>{name}</div>
    {List.map(({setData, Title},index)=>(
     <li className="checkbox" key={index}>
        <input
          type="radio"
          name={name}
          checked={isChecked[index]}
          onChange={e => {set(setData); handleOnChange(index)}}></input>
        {Title}
     </li>
    ))}
  </ul>
  )
}


export default SortByCheckboxes