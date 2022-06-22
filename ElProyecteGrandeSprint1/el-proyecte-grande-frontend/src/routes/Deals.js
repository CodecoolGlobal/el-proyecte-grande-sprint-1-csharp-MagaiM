import React, { Component, useEffect } from 'react';
import { useState } from "react";
import { DropdownButton, Dropdown, Table, FormCheck} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import DealCards from './DealCards';
import DealsTable from './DealsTable';

const Deals = () => {
    const [dealsData, setDealsData] = useState([]);
    const [isChecked, setIsChecked] = useState(new Array(2).fill(false));
    const [sortBy, setSortBy] = useState("Deal Rating");
    const [pageSize, setPageSize] = useState(60);
    const [filterDirection, setFilterDirection] = useState(0);
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
    fetchData(Url)
    .then(dealsData => {
        setDealsData(dealsData);
    })
}, [Url]);

useEffect(() => {
  if(isChecked){
    setUrl(`https://localhost:7064/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)
  }else{
    setUrl(lastUrl)
  }
}, [isChecked]);


  const handleOnChange = (value) => {
    const updatedCheckedState = isChecked.map((item, index) =>
      index === value ? !item : item
    );
    setIsChecked(updatedCheckedState);
    };
  const dealsPageContent = 
  (
  
  <div className="deals-page-container">
    <div className='filter-bar'>
      <div className="checkbox">
      <input
        type="checkbox"
        checked={isChecked[0]}
        onChange={e => {setSortBy("Title"); handleOnChange(0)}}
        
      />Title
      </div>
      <div className="checkbox">
      <input
        type="checkbox"
        checked={isChecked[1]}
        onChange={e => {setFilterDirection(1); handleOnChange(1)}}
        
      />Sort Direction
      </div>
    </div>
    <div>
      <DealCards dealsData={dealsData}></DealCards>
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