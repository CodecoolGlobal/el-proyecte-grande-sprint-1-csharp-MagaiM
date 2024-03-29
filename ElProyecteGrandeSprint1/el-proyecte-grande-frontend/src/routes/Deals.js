import React, { useEffect } from 'react';
import { useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import DealsTable from '../components/DealsTable';
import { sortByList } from '../utils/Sortby';
import { filterDirectionList } from '../utils/FilterDirection';
import { storeNames } from '../utils/StoreNames';
import Checkboxes from '../components/Checkboxes';
import PropTypes from 'prop-types';
import AuthService from '../services/auth.service';

const Deals = ({ fetchData }) => {
  const currentUser = AuthService.getCurrentUser();
  const [dealsData, setDealsData] = useState([]);
  const [isChecked, setIsChecked] = useState(new Array(sortByList.length + filterDirectionList.length + storeNames.length).fill(false));
  const [baseSortBy] = useState("Deal Rating");
  const [sortBy, setSortBy] = useState("Deal Rating");
  const [pageSize] = useState(60);
  const [baseFilterDirection] = useState(0);
  const [filterDirection, setFilterDirection] = useState(0);
  const [baseStoreId] = useState("")
  const [storeId, setStoreId] = useState("")
  const [Url, setUrl] = useState(`https://el-proyecte-grande-kvm-gaming.herokuapp.com/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)

  useEffect(() => {
    fetchData(Url)
      .then(dealsData => {
        setDealsData(dealsData);
      })
  }, [Url]);

  useEffect(() => {
    setUrl(`https://el-proyecte-grande-kvm-gaming.herokuapp.com/Deals?sortBy=${sortBy}&pageSize=${pageSize}&desc=${filterDirection}&storeId=${storeId}`)
  }, [isChecked]);

  if (currentUser === null)
    window.location.href = '/login';

  const handleOnChange = (checkboxIndex) => {
    const updatedCheckedState = isChecked.map((item, index) => {
      if (index === checkboxIndex) {
        item = true;
      } else {
        item = false;
      }
    }
    );
    updatedCheckedState.map((item, index) => {
      if (checkboxIndex === index && item === false) {
        switch (index) {
          case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8:
            setSortBy(baseSortBy)
            break;
          case 9: case 10:
            setFilterDirection(baseFilterDirection)
            break;
          case 11: case 12: case 13: case 14: case 15: case 16: case 17: case 18: case 19: case 20: case 21:
          case 22: case 23: case 24: case 25: case 26: case 27: case 28: case 29: case 30: case 31: case 32:
            setStoreId(baseStoreId)
            break;
        }
      }
    });
    setIsChecked(updatedCheckedState);
  };


  const dealsPageContent =
    (
      <div className="deals-page-container">
        <div className='filter-bar'>
          <Checkboxes handleOnChange={handleOnChange} set={setSortBy} isChecked={isChecked} List={sortByList} name={"SortBy"}></Checkboxes>
          <Checkboxes handleOnChange={handleOnChange} set={setFilterDirection} isChecked={isChecked} List={filterDirectionList} name={"Direction"}></Checkboxes>
          <Checkboxes handleOnChange={handleOnChange} set={setStoreId} isChecked={isChecked} List={storeNames} name={"Stores"}></Checkboxes>
        </div>
        <div className='divider'></div>
        <div className='cards-div'>
          <DealsTable dealsData={dealsData}></DealsTable>
        </div>
      </div>
    )

  return dealsPageContent;
}

Deals.propTypes = {
  fetchData: PropTypes.func
};

export default Deals