import React from 'react';

const DealsTable = ({dealsData}) => {

  return (
        <div className="table-wrapper" style={{width: '100%'}}>
                    {dealsData.map((deals) => (
                        <div key={deals.Title} className="col-md-auto col-md-auto bg-dark deal-div">
                            <div className="deal-card">
                                <div className='image-div'>
                                  <img className="product-image" src={String(deals.Image)}/>
                                </div>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F', height:'auto'}}>
                                    <h6 className=" deal-title">{deals.Title}</h6>
                                    <p className="card-text">Store: {deals.StoreName}</p>
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