<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PruebaLABS.Vista.Inicio" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>LABS - Soluciones de Logística y Transporte</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f5f7f6;
        }

        .navbar-custom {
            background-color: #ffffff !important;
            box-shadow: 0px 3px 12px rgba(0,0,0,0.08);
            padding: 12px 0;
        }

        .navbar-brand {
            letter-spacing: 1px;
        }

        #inicio {
            min-height: 60vh;
            background-image: url('/Vista/imagenes/mula.jpg');
            background-size: cover; 
            background-position: center; 
            background-repeat: no-repeat; 
            position: relative;
        }


            #inicio::before {
                content: '';
                position: absolute;
                inset: 0;
                background: rgba(0, 0, 0, 0.55);
            }

            #inicio .hero-content {
                position: relative;
                z-index: 2;
                padding-top: 80px;
            }

        .card-soft {
            background: #ffffff;
            border-radius: 15px;
            padding: 30px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
            transition: transform .2s;
        }

            .card-soft:hover {
                transform: translateY(-5px);
            }

        .icon-big {
            font-size: 48px;
            color: #2E7D32;
            margin-bottom: 15px;
        }

        footer {
            background: #ffffff;
            box-shadow: 0px -2px 10px rgba(0,0,0,0.05);
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-lg navbar-custom sticky-top">
            <div class="container">
                <a href="Inicio.aspx" class="navbar-brand fw-bold fs-3 text-success">
                    <i class="bi bi-truck-front-fill me-2"></i>LABS
                </a>

                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarMenu">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarMenu">
                    <ul class="navbar-nav ms-auto fw-semibold">
                        <li class="nav-item"><a class="nav-link text-success active" href="#inicio">Inicio</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#about">Nosotros</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#services">Servicios</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#process">Procesos</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#contact">Informacion</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#trabaja">Trabaja con Nosotros</a></li>

                        <li id="liLogin" runat="server" class="nav-item ms-lg-3">
                            <a class="btn btn-outline-success" href="Login.aspx">Iniciar sesión</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <section id="inicio" class="d-flex align-items-center">
            <div class="container text-center hero-content text-white">
                <h1 class="display-3 fw-bold">LABS</h1>
                <p class="lead fs-4 mb-4">Soluciones de logística y transporte con precisión y confianza</p>
                <a href="#contact" class="btn btn-success btn-lg px-5 py-3">Solicitar Cotización</a>
            </div>
        </section>

        <section id="about" class="py-5">
            <div class="container text-center">
                <h2 class="fw-bold mb-5 text-success">Sobre Nosotros</h2>
                <div class="row g-4">

                    <div class="col-lg-4">
                        <div class="card-soft">
                            <i class="fas fa-truck-fast icon-big"></i>
                            <h3 class="fw-semibold">Nuestra Pasión</h3>
                            <p>Movemos carga de forma segura, rápida y precisa usando tecnología moderna.</p>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <div class="card-soft">
                            <i class="fas fa-user-tie icon-big"></i>
                            <h3 class="fw-semibold">Expertos en Logística</h3>
                            <p>Un equipo altamente capacitado garantiza un servicio profesional.</p>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <div class="card-soft">
                            <i class="fas fa-check icon-big"></i>
                            <h3 class="fw-semibold">Nuestros Valores</h3>
                            <p>Compromiso, responsabilidad e innovación en cada movimiento.</p>
                        </div>
                    </div>

                </div>
            </div>
        </section>

        <section id="services" class="py-5 bg-light text-center">
            <div class="container">
                <h2 class="fw-bold mb-4 text-success">Nuestros Servicios</h2>

                <div class="row g-4">
                    <div class="col-md-4">
                        <div class="card-soft">
                            <i class="fas fa-truck-moving icon-big"></i>
                            <h4 class="fw-semibold">Transporte de Carga</h4>
                            <p>Movilizamos mercancías de forma segura por todo el país.</p>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="card-soft">
                            <i class="fas fa-bolt icon-big"></i>
                            <h4 class="fw-semibold">Envíos Express</h4>
                            <p>Entregas rápidas y confiables con monitoreo en tiempo real.</p>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="card-soft">
                            <i class="fas fa-boxes-stacked icon-big"></i>
                            <h4 class="fw-semibold">Gestión de Inventarios</h4>
                            <p>Control eficiente de almacenamiento y distribución.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="process" class="py-5 text-center">
            <div class="container">
                <h2 class="fw-bold mb-4 text-success">Nuestro Proceso</h2>

                <div class="row g-4">
                    <div class="col-md-4">
                        <i class="fas fa-phone-volume icon-big"></i>
                        <h4 class="fw-semibold">1. Solicitud</h4>
                        <p>Registra el servicio que necesitas en la plataforma.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fas fa-truck-arrow-right icon-big"></i>
                        <h4 class="fw-semibold">2. Asignación</h4>
                        <p>Seleccionamos el vehículo adecuado según tu necesidad.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fas fa-location-dot icon-big"></i>
                        <h4 class="fw-semibold">3. Seguimiento</h4>
                        <p>Monitorea tu carga en tiempo real durante el viaje.</p>
                    </div>
                </div>
            </div>
        </section>

        <section id="contact" class="py-5">
            <div class="container">
                <h2 class="fw-bold text-success mb-5 text-center">Información</h2>

                <div class="row g-4 justify-content-center">
                    <div class="col-lg-6">
                        <div class="card-soft text-start text-center">
                            <h4 class="fw-bold mb-3">Información</h4>
                            <p><i class="fa fa-map-marker-alt text-success me-2"></i>Cra. 39 #10-1, Duitama, Boyacá</p>
                            <p><i class="fa fa-phone text-success me-2"></i>6087654504</p>
                            <p><i class="fa fa-envelope text-success me-2"></i>contacto.labs@gmail.com</p>
                            <p><i class="fa fa-clock text-success me-2"></i>Lunes a Sábado 8:00am - 6:00pm</p>
                            <div class="mt-3">
                                <iframe
                                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3969.3350404561547!2d-73.027305!3d5.80827!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e6a3f6322f3e2bf%3A0x46d1b815677d0db!2sCra.%2039%20%2310-1%2C%20Duitama%2C%20Boyac%C3%A1!5e0!3m2!1ses-419!2sco!4v1764001179329!5m2!1ses-419!2sco"
                                    width="100%"
                                    height="350"
                                    style="border: 0; border-radius: 12px;"
                                    allowfullscreen=""
                                    loading="lazy"
                                    referrerpolicy="no-referrer-when-downgrade"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>





        <section id="trabaja" class="position-relative text-white text-center" style="height: 80vh;">
            <img src="https://www.lctransportes.com/wp-content/uploads/2021/01/graneles2.jpg"
                class="img-fluid w-100 h-100 object-fit-cover" style="opacity: 0.35;" />

            <div class="position-absolute top-0 start-0 w-100 h-100" style="background-color: rgba(0,0,0,0.5);"></div>

            <div class="position-absolute top-50 start-50 translate-middle w-100 px-3">
                <h2 class="fw-bold mb-3">Trabaja con Nosotros</h2>
                <p class="lead mb-4">
                    ¿Tienes tractomula? Únete a LABS y accede a cargas en todo el país.
                </p>
                <div class="container">
                    <div class="row justify-content-center mb-3">
                        <div class="col-md-6">
                            <input type="email" class="form-control form-control-lg mb-3" placeholder="Correo electrónico" />
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-md-3">
                            <button class="btn btn-success btn-lg w-100 fw-bold">Postulate</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <footer class="text-center py-4">
            <div class="container">
                <h4 class="fw-bold mb-3 text-success">Síguenos</h4>

                <div class="mb-3">
                    <a href="#" class="text-success mx-2"><i class="bi bi-facebook fs-3"></i></a>
                    <a href="#" class="text-success mx-2"><i class="bi bi-instagram fs-3"></i></a>
                    <a href="#" class="text-success mx-2"><i class="bi bi-twitter-x fs-3"></i></a>
                    <a href="#" class="text-success mx-2"><i class="bi bi-linkedin fs-3"></i></a>
                    <a href="#" class="text-success mx-2"><i class="bi bi-youtube fs-3"></i></a>
                </div>

                <hr class="border-success" />

                <p class="mb-0 text-success fw-semibold">
                    <strong>LABS Logística</strong> — Moviendo el progreso de Colombia
                </p>
            </div>
        </footer>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>
