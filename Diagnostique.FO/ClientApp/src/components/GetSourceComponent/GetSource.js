import React, { Component } from 'react';
import Button from 'react-bootstrap/lib/Button';
import Table from 'react-bootstrap/lib/Table';
import Panel from 'react-bootstrap/lib/Panel';
import ListGroup from 'react-bootstrap/lib/ListGroup';
import ListGroupItem from 'react-bootstrap/lib/ListGroupItem';

import DatePicker from 'react-datepicker';
import moment from 'moment';
import 'react-datepicker/dist/react-datepicker.css';

export class GetSource extends React.Component {

constructor(props) {
    super(props); 
    
    this.handleChangeSCD = this.handleChangeSCD.bind(this);
    this.handleChangeECD = this.handleChangeECD.bind(this);
    this.handleChangeSFD = this.handleChangeSFD.bind(this);
    this.handleChangeEFD = this.handleChangeEFD.bind(this);

    this.state = { donnesDeSource: [], loading: false, premierDateCom: null, derniereDateCom: null, premierFactureDateCom: null, DernierFactureDateCom: null, nom: '', prenom: '', secteur: '' };
    }


    handleChangeFN(value) { 
       this.setState({ nom: value });
    }
    handleChangeLN(value) {
        this.setState({ prenom: value });
    }
    handleChangeSEC(value) {
        this.setState({ secteur: value });
    }
    handleChangeSCD(value) {   
        if (typeof value === 'undefined') { 
            this.setState({
                premierDateCom: ''
            }, () => console.log(this.state.premierDateCom));
        } else {           
            this.setState({
                premierDateCom: value
            }, () => console.log(this.state.premierDateCom)); // This will show the updated state when state is set.
        }
    }
    handleChangeECD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                derniereDateCom: ''
            }, () => console.log(this.state.derniereDateCom));
        } else {
            this.setState({
                derniereDateCom: value
            }, () => console.log(this.state.derniereDateCom)); // This will show the updated state when state is set.
        }
    }
    handleChangeSFD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                premierFactureDateCom: ''
            }, () => console.log(this.state.premierFactureDateCom)); 
        } else {
            this.setState({
                premierFactureDateCom: value
            }, () => console.log(this.state.premierFactureDateCom)); // This will show the updated state when state is set.
        }
    }
    handleChangeEFD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                DernierFactureDateCom: ''
            }, () => console.log(this.state.DernierFactureDateCom)); 
        } else {
            this.setState({
                DernierFactureDateCom: value
            }, () => console.log(this.state.DernierFactureDateCom)); // This will show the updated state when state is set.
        }       
    }

    AppelApiInsertionSource(e) {
        fetch('api/DestinationFo/AjoutDonneesDITAsync', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.state.donnesDeSource)
        }).then((response) => response.json())
            .then((responseData) => {
                if (responseData == true) {
                    this.setState({ donnesDeSource: [], loading: false });
                    //alert("Insertions");
                }   
        }); 
    }

    AppelApiRecuperationSource(e) {      
        this.setState({
            loading: true
        });
      
        fetch(`api/SourceFo/SelectionDonneesSourceAsync?premierDateCom=${moment(this.state.premierDateCom).format('LLLL')}&derniereDateCom=${moment(this.state.derniereDateCom).format('LLLL')}&premierFactureDateCom=${moment(this.state.premierFactureDateCom).format('LLLL')}&DernierFactureDateCom=${moment(this.state.DernierFactureDateCom).format('LLLL')}&nom=${this.state.nom}&prenom=${this.state.prenom}&secteur=${this.state.secteur}`)
            .then(response => response.json())
            .then(data => { 
                this.setState({ donnesDeSource: data, loading: false });
                console.log(this.state.donnesDeSource)
               
            }).catch((error) => {
                this.setState({ donnesDeSource: [], loading: false });
            });
    }
    
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : GetSource.renderDonneeTable(this.state.donnesDeSource);
        return (
            <div>
                <h1>Récupération des données à partir de la Source</h1>
                <p>Récuperation des données à partir d'une base de données de source.</p>
                <br />
                <form>
                    <Table striped bordered condensed hover>
                        <thead>
                            <tr>
                                <th>Première date Commande</th>
                                <th>Dernière date Commande </th>
                                <th>Première date Facture</th>
                                <th>Dernière date Facture</th>
                                <th>Nom</th>
                                <th>Prénom</th>
                                <th>Secteur</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>                               
                                <td> <DatePicker
                                    selected={this.state.premierDateCom}
                                    onChange={this.handleChangeSCD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..." /></td>
                                <td> <DatePicker
                                    selected={this.state.derniereDateCom}
                                    onChange={this.handleChangeECD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..." /></td>
                                <td> <DatePicker
                                    selected={this.state.premierFactureDateCom}
                                    onChange={this.handleChangeSFD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..." /></td>
                                <td>   <DatePicker
                                    selected={this.state.DernierFactureDateCom}
                                    onChange={this.handleChangeEFD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..." /></td>

                                <td>  <input type="text" value={this.state.nom} onChange={e => this.handleChangeFN(e.target.value)} name="usr_first_name" /></td>
                                <td> <input type="text" value={this.state.prenom} onChange={e => this.handleChangeLN(e.target.value)} name="usr_last_name" /></td>
                                <td> <input type="text" value={this.state.secteur} onChange={e => this.handleChangeSEC(e.target.value)} name="usr_sect" /></td>
                            </tr>
                        </tbody>
                    </Table>
                    <div align="right">
                        <Button bsStyle="primary" bsSize="small" onClick={this.AppelApiRecuperationSource.bind(this)} value="">Filtrer</Button>
                    </div>                 
                    <br />

                    {contents}

                    <div align="right">
                        <Button bsStyle="success" bsSize="small" onClick={this.AppelApiInsertionSource.bind(this)} value="">Pousser</Button>
                    </div>  

                     
  
                </form>

            </div>
        );
    }



    static renderDonneeTable(donnesDeSource) {
        return (
            <Table striped bordered condensed hover>
                <thead>
                    <tr>
                        <th>Colonne Id</th>
                        <th>Nom Utilsateur</th>
                        <th>Prénom Utilisateur</th>
                        <th>Secteur</th>
                        <th>Catalogue</th>
                        <th>Catalogue Autre</th>
                        <th>Version</th>
                        <th>Id Premier Com</th>
                        <th>Date Premier Com</th>
                        <th>Id Dernier Com</th>
                        <th>Date Dernier Com</th>
                        <th>Id Premier Fact</th>
                        <th>Date Premier Fact</th>
                        <th>Id Dernier Fact</th>
                        <th>Date Dernier Fact</th>
                        <th>Date Visite</th>
                        <th>Tables</th>
                        <th>Ligne</th>
                        <th>Table/Indexes</th>
                    </tr>
                </thead>
                <tbody>
                    {donnesDeSource.map(ds =>
                        <tr key={ds.colId}>
                            <td>{ds.colId}</td>
                            <td>{ds.sourceFormarteUtilNom}</td>
                            <td>{ds.sourceFormarteUtilPrenom}</td>
                            <td>{ds.sourceFormarteSectNom}</td>
                            <td>{ds.sourceFormarteCatalPrincipal}</td>
                            <td>{ds.sourceFormarteCatalAutrePrincipal}</td>
                            <td>{ds.sourceFormarteVersNom}</td>
                            <td>{ds.SourceFormarteComPremierDateId}</td>
                            <td>{moment(ds.sourceFormarteComPremierDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComDernierDateId}</td>
                            <td>{moment(ds.sourceFormarteComDerniereDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComPremierFactureDateId}</td>
                            <td>{moment(ds.sourceFormarteComPremierFactureDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComDernierFactureDateId}</td>
                            <td>{moment(ds.sourceFormarteComDerniereFactureDate).format('DD/MM/YYYY')}</td>
                            <td>{moment(ds.sourceFormarteVisDate).format('DD/MM/YYYY')}</td>
                            <td>
                                <ListGroup>
                                    {
                                        ds.sourceFormarteTabNom.map((nt) =>
                                            <ListGroupItem bsStyle="success"> {nt} </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                            <td>
                                <ListGroup>
                                    {
                                        ds.sourceFormarteTabLigneComptage.map((ligne) =>
                                            <ListGroupItem bsStyle="success"> {ligne} </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                            <td>
                                <ListGroup bsStyle="success">
                                    {
                                        ds.sourceFormarteTabNomIndex.map((ligne) =>
                                            <ListGroupItem bsStyle="success">{ligne.tableNom} : {
                                                ligne.indexes.map((index) =>
                                                    <ListGroupItem bsStyle="info"> {index}  </ListGroupItem>
                                                )
                                            }
                                            </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }
}
