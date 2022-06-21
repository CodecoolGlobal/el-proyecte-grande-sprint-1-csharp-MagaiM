import React from 'react';
import { useState, useEffect } from "react";
import { useRef } from 'react';
import PropTypes from 'prop-types';

const DealsTable = ({dealsData}) => {

  return (
        <div className="table-wrapper" style={{width: '100%'}}>
                    {dealsData.map((deals) => (
                        <div key={deals.Title} className="col-md-auto col-md-auto bg-dark" style={{display: 'inline-block', width: '220px', height: '350px'}}>
                            <div className="deal-card" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 10px 50px 10px'}}>
                                <div className='image-div'>
                                  <img className="product-image" src={String(deals.Image)}/>
                                </div>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F', height:'175px'}}>
                                    <p className=" deal-title">{deals.Title}</p>
                                        <p className="card-text deal-info">Deal rating: {deals.DealRating}({Math.floor(100-(deals.SalePrice/deals.NormalPrice)*100)}%)</p>
                                        <p className="card-text price-info">Sale price: ${String(deals.SalePrice)}</p>
                                        <p className="card-text price-info">Normal price: ${String(deals.NormalPrice)}</p>
                                </div>
                            </div>
                            <br/>
                        </div>
                       
                    ))}
                </div>
  )
}


export default DealsTable