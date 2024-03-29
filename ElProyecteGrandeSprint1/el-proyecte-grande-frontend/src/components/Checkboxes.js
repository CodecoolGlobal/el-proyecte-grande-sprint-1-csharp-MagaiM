import React from 'react';

const SortByCheckboxes = ({ handleOnChange, set, isChecked, List, name }) => {
  return (
    <ul className='checkbox-div'>
      <div className='checkbox-div-title'>{name}:</div>
      {List.map(({ setData, Title }, index) => (
        <li className="checkbox" key={index}>
          <input className='inpu'
            type="radio"
            name={name}
            checked={isChecked[index]}
            onChange={() => { set(setData); handleOnChange(index) }}></input>
          {` ${Title}`}
        </li>
      ))}
    </ul>
  )
}


export default SortByCheckboxes