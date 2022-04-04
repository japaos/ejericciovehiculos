//funcion para definir+ prioridad y riesgo
var urlPersona="https://localhost:7268/api/Vehiculo";

function riesgo(ed,exr){
    var edad=parseFloat(ed);
    var extra=parseFloat(exr);
    var r=1;
    var u=0;
    if(edad<6){
        r=extra+3;
    }
    else if(edad<13){
        r=extra+2;
    }
    else if(edad<16){
        r=extra+1;
    }
    else if(edad<41){
        r=2
        if(extra>0){
            r=(edad/4)+2;
        }
    }
    else if(edad<60){
        r=(edad/30)+3;
    }
    else{
        r=(edad/30)+3;
        if(extra!=0){r=(edad/20)+4;}
    }
    if(edad<41){
        u=(edad*r)/100
    }
    else{
        u=((edad*r)/100)+5.3;
    }
    return ([r,u]);
}
$(document).ready(function(){
    console.log("jquery colgado listo");
    $("#busqueda").keyup(function(e){
        let search=$("#busqueda").val();
        $.ajax({
            url:"consulta.php",
            type:"POST",
            data:{
                buscar:search,
                proceso:"buscar1"
            },
            success: function(response){
                let tareas= JSON.parse(response);
                console.log(tareas);
            }

        })
    });
    
    //cambio de formulario de paciente a doctor
    $( "#seleccion" ).change(function() {       
        var seleccion=$("#seleccion").val();
        if(seleccion=="Paciente"){
            document.getElementById("task_form_paciente").style.display = "block";
            document.getElementById("task_form_consulta").style.display = "none";
        }
        else{
            document.getElementById("task_form_paciente").style.display = "none";
            document.getElementById("task_form_consulta").style.display = "block";
        }
      });    

          //cambio de vista ventana registro
    $( "#VentanaR" ).click(function() {       
        document.getElementById("Vista_funciones").style.display = "block";
        document.getElementById("Vista_espera").style.display = "none";
        document.getElementById("Vista_pendiente").style.display = "none";

      });    


          //cambio de vista ventana pendientes
    $( "#VentanaP" ).click(function() {       
        document.getElementById("Vista_funciones").style.display = "none";
        document.getElementById("Vista_espera").style.display = "none";
        document.getElementById("Vista_pendiente").style.display = "block";
        $.ajax({
            url:"conf_ventana.php",
            type:"POST",
            data:{
                buscar:"de",
                proceso:'pendiente',
            },
            success: function(response){
                $("#TP_Pendiente").html(response);
                console.log(response);
            },
            error: function(error){
                console.log("EROR  EDCONTDRADO");
                console.log(error)
            }
        })

      });   

          //cambio de vista espera
    $( "#VentanaE" ).click(function() {       
        document.getElementById("Vista_funciones").style.display = "none";
        document.getElementById("Vista_espera").style.display = "block";
        document.getElementById("Vista_pendiente").style.display = "none";
        $.ajax({
            url:"consulta_mayores.php",
            type:"POST",
            data:{
                buscar:"de",
                proceso:'proceso',
            },
            success: function(response){
                $("#TP_Espera").html(response);
                console.log(response);
            },
            error: function(error){
                console.log("EROR  EDCONTDRADO");
                console.log(error)
            }
        })
      });     


//listar todos los pacientes    
    $("#listar").click(function(){
        let valor=$("#historia2").val();
        console.log(valor);
        if($.isNumeric (valor)){
        $.ajax({
            url:"consulta.php",
            type:"POST",
            data:{
                buscar:"de",
                proceso:valor,
            },
            success: function(response){
                 $("#mostrador").html(response);
                console.log(response);
            },
            error: function(error){
                console.log("EROR  EDCONTDRADO");
                console.log(error)
            }
        })}
        else{
            alert("Valor invalido");
            $("#historia2").val("0");
        }
    });

//listar todos los pacientes fumadores urgentes   
    $("#l_fumador").click(function(){
        $.ajax({
            url:"consulta_fumadores.php",
            type:"POST",
            data:{
                buscar:"de",
                proceso:'proceso',
            },
            success: function(response){
                $("#mostrador").html(response);
                console.log(response);
            },
            error: function(error){
                console.log("EROR  EDCONTDRADO");
                console.log(error)
            }
        })
    });

//listar al paciente mas anciano
$("#anciano").click(function(){
    $.ajax({
        url:"consulta_mayores.php",
        type:"POST",
        data:{
            buscar:"de",
            proceso:'proceso',
        },
        success: function(response){
            $("#mostrador").html(response);
            console.log(response);
        },
        error: function(error){
            console.log("EROR  EDCONTDRADO");
            console.log(error)
        }
    })
});



$("#mejor").click(function(){
    $.ajax({
        url:"consulta_top.php",
        type:"POST",
        data:{
            buscar:"de",
            proceso:'proceso',
        },
        success: function(response){
            //$("#mostrador").html(response);
            //console.log(response);
            alert(response);
        },
        error: function(error){
            console.log("EROR  EDCONTDRADO");
            console.log(error)
        }
    })
});


$("#libera").click(function(){
    $.ajax({
        url:"libera_c.php",
        type:"POST",
        data:{
            buscar:"de",
            proceso:'proceso',
        },
        success: function(response){
            //$("#mostrador").html(response);
            //console.log(response);
            alert(response);
        },
        error: function(error){
            console.log("EROR  EDCONTDRADO");
            console.log(error)
        }
    })
});


//deteccion de boton de guardar paciente
    $("#almacenar").click(function(){
        let no=$("#Nombre").val();   //edad del paciente
        let ap=$("#Apellido").val();                //historia clinica
        let iden=$("#Identificacion").val();                  //nombre paciente
        let ex=$("#extra").val();
        if(no==''||ap==''||iden==''){      //validacion de campos llenados por el usuario   
                alert("falta por llenar, la edad debe ser numero>1");
        }
        else{var f=false;
                $.ajax({                            //solicitud ajax para conexion a la BD
                    url:urlPersona,        //conexion al servidor para almacenamiento de datos
                    type:"POST",                    
                    data:{                          //objeto data con formato json para enviar parametros 
                        "nombre": no,
                        "apellido": ap,
                        "identificacion": iden,
                        "id": 0
                    },
                    success: function(response){    //recepcion de respuesta    
                        alert(response);
                    },


                });
        }

    });

//deteccion de boton de guardar consulta
$("#almacenar_c").click(function(){
    let id=$("#Id_Consulta").val();   //edad del paciente
    let no=$("#nombre_medico").val();   //edad del paciente
    let pa=$("#numero_pacientes").val();                //historia clinica
    let est=$("#estado_consulta").val();                  //nombre paciente
    let t_c=$("#tipo_consulta").val();
    if(!$.isNumeric(id)||no==''|| id<0||!$.isNumeric(pa)||id==''){      //validacion de campos llenados por el usuario   
            alert("falta por llenar, revise el formulario porfavor"+id+no+pa+est+t_c);
    }
    else{ 
            $.ajax({                            //solicitud ajax para conexion a la BD
                url:"guardar_datos.php",        //conexion al servidor para almacenamiento de datos
                type:"POST",                    
                data:{                          //objeto data con formato json para enviar parametros 
                    nombre:no,
                    id_c:id,
                    paciente:pa,
                    estado:est,
                    t_con:t_c,
                    proceso:"guardar_c"
                },
                success: function(response){    //recepcion de respuesta    
                    alert(response);
                },


            });
    }

});

//Atender paciente
$("#atender").click(function(){
    $.ajax({                            //solicitud ajax para conexion a la BD
        url:"atender.php",        //conexion al servidor para almacenamiento de datos
        type:"POST",                    
        data:{                          //objeto data con formato json para enviar parametros 
            proceso:"atender1"
        },
        success: function(response){    //recepcion de respuesta    
            console.log(response);
        }
    });
});

// lectura de  edad para parametro particular segun la edad
    console.log("salio actualizado pgp");
    $("#edad").keyup(function(){
        let ed=$("#edad").val();
        if($.isNumeric(ed) && ed>-1 ){
            if(ed<16){
                $("#extra").attr("placeholder","Ingrese Indice peso estatura valor 1-4");
            }
            else if(ed<41){
                $("#extra").attr("placeholder","Ingrese Si es fumador");
                alert("Ingrese el numero de años como fumador\n Si no fuma ingrese 0");
            } 
            else{
                $("#extra").attr("placeholder","Ingrese  1 si tiene dieta");
            }
        }
        else{console.log("incorrecto");}
    });
    $("#extra").keyup(function(){
        var p=parseInt($("#extra").val(),10);
        var ed=parseInt($("#edad").val(),10);
        if($.isNumeric(ed)){
            if(ed<16 && (p>4 || p<1)){
                alert("Debe ser un numero 1-4");
                $("#extra").val("1");
            }
            else if(ed<41&&p<0){
                $("#extra").val(0);
            } 
        }
        else{alert("Este valor debe ser numerico");}
    });
});

