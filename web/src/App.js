import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.js';

import React, { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
  const URLPrefix = "https://localhost:7057/exchangeRates/table/";

  const [tableA, setTableA] = useState([]);
  const [tableB, setTableB] = useState([]);
  const [tableC, setTableC] = useState([]);

  useEffect(() => {
    // maybe use getAll istead
    LoadTableA();
    LoadTableB();
    LoadTableC();
  }, []);

  function LoadTableA() {
    axios.get(URLPrefix + 'A')
      .then(response => {
        setTableA(response.data[0]);
        // do sth more
      })
      .catch(error => {
        console.error(error);
      });
  }

  function LoadTableB() {
    axios.get(URLPrefix + 'B')
      .then(response => {
        setTableB(response.data[0]);
        // do sth more
      })
      .catch(error => {
        console.error(error);
      });
  }

  function LoadTableC() {
    axios.get(URLPrefix + 'C')
      .then(response => {
        setTableC(response.data[0]);
        // do sth more
      })
      .catch(error => {
        console.error(error);
      });
  }

  return (
    <div className="App">
      <nav>
        <div className="nav nav-tabs" id="nav-tab" role="tablist">
          <button className="nav-link active" id="nav-table-a-tab" data-bs-toggle="tab" data-bs-target="#nav-table-a" type="button" role="tab" aria-controls="nav-table-a" aria-selected="true">Tabela A</button>
          <button className="nav-link" id="nav-table-b-tab" data-bs-toggle="tab" data-bs-target="#nav-table-b" type="button" role="tab" aria-controls="nav-table-b" aria-selected="false">Tabela B</button>
          <button className="nav-link" id="nav-table-c-tab" data-bs-toggle="tab" data-bs-target="#nav-table-c" type="button" role="tab" aria-controls="nav-table-c" aria-selected="false">Tabela C</button>
        </div>
      </nav>
      <div className="tab-content" id="nav-tabContent">
        <div className="tab-pane fade show active" id="nav-table-a" role="tabpanel" aria-labelledby="nav-table-a-tab">
          {/* FIXEME: Put table into component */}
          <h2>Tabela nr {tableA.no} z dnia {tableA.effectiveDate?.substring(0, 10)}</h2>
          <table className="table table-striped table-hover">
            <thead>
              <tr>
                <td>Nazwa waluty</td>
                <td>Kod waluty</td>
                <td>Kurs średni</td>
              </tr>
            </thead>
            <tbody>
              {tableA?.rates?.map((rate, index) => {
                return (
                  <tr key={index}>
                    <td>{rate.currency}</td>
                    <td>{rate.code}</td>
                    <td>{rate.mid}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
        <div className="tab-pane fade" id="nav-table-b" role="tabpanel" aria-labelledby="nav-table-b-tab">
          <h2>Tabela nr {tableB.no} z dnia {tableB.effectiveDate?.substring(0, 10)}</h2>
          <table className="table table-striped table-hover">
            <thead>
              <tr>
                <td>Nazwa waluty</td>
                <td>Kod waluty</td>
                <td>Kurs średni</td>
              </tr>
            </thead>
            <tbody>
              {tableB?.rates?.map((rate, index) => {
                return (
                  <tr key={index}>
                    <td>{rate.currency}</td>
                    <td>{rate.code}</td>
                    <td>{rate.mid}</td>
                  </tr>
                );
              })}
            </tbody>
          </table></div>
        <div className="tab-pane fade" id="nav-table-c" role="tabpanel" aria-labelledby="nav-table-c-tab">
          <h2>Tabela nr {tableC.no} z dnia {tableC.tradingDate?.substring(0, 10)} obowiązująca od dnia {tableC.effectiveDate?.substring(0, 10)}</h2>
          <table className="table table-striped table-hover">
            <thead>
              <tr>
                <td>Nazwa waluty</td>
                <td>Kod waluty</td>
                <td>Kurs kupna</td>
                <td>Kurs sprzedaży</td>
              </tr>
            </thead>
            <tbody>
              {tableC?.rates?.map((rate, index) => {
                return (
                  <tr key={index}>
                    <td>{rate.currency}</td>
                    <td>{rate.code}</td>
                    <td>{rate.bid}</td>
                    <td>{rate.ask}</td>
                  </tr>
                );
              })}
            </tbody>
          </table></div>
      </div>
    </div>
  );
}

export default App;
