import React, { Component, useEffect } from 'react';
import { useState } from "react";
import { DropdownButton, Dropdown, Table, FormCheck} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import DealCards from '../components/DealCards';
import DealsTable from '../components/DealsTable';
import {sortByList} from '../utils/Sortby';
import { filterDirectionList } from '../utils/FilterDirection';
import { storeNames } from '../utils/StoreNames';

const Deals = () => {
    const [dealsData, setDealsData] = useState([]);
    const [isChecked, setIsChecked] = useState(new Array(sortByList.length+filterDirectionList.length).fill(false));
    const [baseSortBy, setBaseSortBy] = useState("Deal Rating");
    const [sortBy, setSortBy] = useState("Deal Rating");
    const [basePageSize, setBasePageSize] = useState(60);
    const [pageSize, setPageSize] = useState(60);
    const [baseFilterDirection, setBaseFilterDirection] = useState(0);
    const [filterDirection, setFilterDirection] = useState(0);
    const [baseStoreId, setBaseStoreId] = useState("")
    const [storeId, setStoreId] = useState("")
    const [lastUrl, setLastUrl] = useState(`https://localhost:7064/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)
    const [Url, setUrl] = useState(`https://localhost:7064/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)
    useEffect(() => {
      fetchData(Url)
      .then(dealsData => {
          setDealsData(dealsData);
      })
  }, [])

    useEffect(() => {
        fetchData('https://localhost:44321/Deals?sortBy=Deal Rating')
        .then(dealsData => {
            setDealsData(dealsData);
        })
    }, [])

useEffect(() => {
    fetchData(Url)
    .then(dealsData => {
        setDealsData(dealsData);
    })
}, [Url]);

useEffect(() => {
  setUrl(`https://localhost:7064/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)
}, [isChecked]);


  const handleOnChange = (checkboxIndex, value) => {
    const updatedCheckedState = isChecked.map((item, index) =>{
      if(index === checkboxIndex){
        item=true;
      }else{
        item=false;
      }
    }
    );
    updatedCheckedState.map((item, index)  =>{
      if(checkboxIndex === index && item === false){
        switch(index){
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
          case 7:
          case 8:
            setSortBy(baseSortBy)
            break;
          case 9:
          case 10:
            setFilterDirection(baseFilterDirection)
            break;
          case 11:
          case 12:
          case 13:
          case 14:
          case 15:
          case 16:
          case 17:
          case 18:
          case 19:
          case 20:
          case 21:
          case 22:
          case 23:
          case 24:
          case 25:
          case 26:
          case 27:
          case 28:
          case 29:
          case 30:
          case 31:
          case 32:
            setStoreId(baseStoreId)
            break;

        }
      }});
    setIsChecked(updatedCheckedState);
    

    };
  const dealsPageContent = 
  (
  
  <div className="deals-page-container">
    <div className='filter-bar'>
      <ul className='checkbox-div'>
        <div className='checkbox-div-title'>SortBy</div>

      {sortByList.map(({setData, Title},index)=>(
         <li className="checkbox" key={index}>
         <input
           type="radio"
           name='sortBy'
           checked={isChecked[index]}
           onChange={e => {setSortBy(setData); handleOnChange(index)}}></input>
         {Title}
         </li>
        ))}
      </ul>
      <ul className='checkbox-div'>
      <div className='checkbox-div-title'>Direction</div>

      {filterDirectionList.map(({setData, Title},index)=>(
        <li className="checkbox" key={sortByList.length+index}>
        <input
          type="radio"
          name='direction'
          checked={isChecked[sortByList.length+index]}
          onChange={e => {setFilterDirection(setData); handleOnChange(sortByList.length+index)}}></input>
        {Title}
        </li>
      ))}
      </ul>
      <ul className='checkbox-div'>
      <div className='checkbox-div-title'>Stores</div>

      {storeNames.map(({setData, Title},index)=>(
        <li className="checkbox" key={sortByList.length+index}>
        <input
          type="radio"
          name='Stores'
          checked={isChecked[sortByList.length+filterDirectionList.length+index]}
          onChange={e => {setStoreId(setData); handleOnChange(sortByList.length+filterDirectionList.length+index)}}></input>
        {Title}
        </li>
      ))}
      </ul>
      <div className='checkbox-div'></div>
    </div>
    <div className='divider'></div>
    <div className='cards-div'>
      {/* <DealCards dealsData={dealsData}></DealCards> */}
      <DealsTable dealsData={dealsData}></DealsTable>
    </div>
  </div>
  )

return dealsPageContent;
}



async function fetchData(url) {
    const response = await fetch(url);
    if (response.ok){
        const data = await response.json();
        return data;
    }
    throw response;
}

export default Deals