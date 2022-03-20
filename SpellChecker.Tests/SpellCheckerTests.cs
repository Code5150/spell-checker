using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpellChecker.Tests
{
    public class SpellCheckerTests
    {
        [Fact]
        public void DefaultTest()
        {
            var dict = new HashSet<string>("rain spain plain plaint pain main mainly the in on fall falls his was".Split(' '));
            var linesToCorrect = new List<string>()
            {
                "hte rame in pain fells",
                "mainy oon teh lain",
                "was hints pliant"
            };
            var checker = new Checker(dict);
            var result = linesToCorrect.Select(line => checker.CheckLine(line.ToLower(), new char[] { ' ' })).Aggregate((first, next) => $"{first} {next}").Trim();
            Assert.Equal("the {rame?} in pain falls {main mainly} on the plain was {hints?} plaint", result);
        }

        [Fact]
        public void OrderTest()
        {
            var dict = new HashSet<string>("rain spain plain plaint pain mainly main tmainy the in on fall falls his was".Split(' '));
            var linesToCorrect = new List<string>()
            {
                "hte rame in pain fells",
                "mainy oon teh lain",
                "was hints pliant"
            };
            var checker = new Checker(dict);
            var result = linesToCorrect.Select(line => checker.CheckLine(line.ToLower(), new char[] { ' ' })).Aggregate((first, next) => $"{first} {next}").Trim();
            Assert.Equal("the {rame?} in pain falls {mainly main tmainy} on the plain was {hints?} plaint", result);
        }

        [Fact]
        public void EmptyDictTest()
        {
            var dict = new HashSet<string>();
            var linesToCorrect = new List<string>()
            {
                "hte rame in pain fells",
                "mainy oon teh lain",
                "was hints pliant"
            };
            var checker = new Checker(dict);
            var result = linesToCorrect.Select(line => checker.CheckLine(line.ToLower(), new char[] { ' ' })).Aggregate((first, next) => $"{first} {next}").Trim();
            Assert.Equal("{hte?} {rame?} {in?} {pain?} {fells?} {mainy?} {oon?} {teh?} {lain?} {was?} {hints?} {pliant?}", result);
        }

        [Fact]
        public void EmptyTextTest()
        {
            var dict = new HashSet<string>();
            var linesToCorrect = new List<string>()
            {
                "",
                "         ",
                null
            };
            var checker = new Checker(dict);
            var result = linesToCorrect.Select(line => checker.CheckLine(line, new char[] { ' ' })).Aggregate((first, next) => $"{first} {next}").Trim();
            Assert.Equal("", result);
        }

        [Fact]
        public void LargeAmountTest()
        {
            var linesToCorrect = new List<string>() {"Lorem ipsum dolor sit amet consectetur adipiscing elit Nullam leo enim dignissim et rutru".Replace('.', ' ').Replace(',', ' '),
                "eget cursus consectetur ipsum Suspendisse posuere eleifend sapien quis ultrices. Donec sed nibh rhoncus, sollicitudin sapien sed".Replace('.', ' ').Replace(',', ' '),
                "scelerisque justo. Donec nec erat sed elit vestibulum pharetra. Integer ut sagittis urna, vitae lobortis eros. Sed id mi non enim".Replace('.', ' ').Replace(',', ' '),
                "maximus faucibus. Proin commodo hendrerit rhoncus. Vivamus congue nisi at ex venenatis lacinia. In hac habitasse platea dictumst".Replace('.', ' ').Replace(',', ' '),
                "In varius sollicitudin dui, sit amet volutpat nunc volutpat vel. Mauris posuere pellentesque urna, volutpat aliquet urna. Morbi".Replace('.', ' ').Replace(',', ' '),
                "lorem lorem, elementum nec porttitor non, iaculis id augue. Integer mattis, eros eu elementum interdum, turpis est volutpat lectus".Replace('.', ' ').Replace(',', ' '),
                "vel semper metus dui a enim. Donec non elementum leo. Morbi hendrerit egestas turpis, non tristique metus porta eu. Aenean luctus".Replace('.', ' ').Replace(',', ' '),
                "sagittis vestibulum. Quisque odio leo, varius eget finibus ut, laoreet ut neque. Ut ultricies, massa id lobortis consequat, massa".Replace('.', ' ').Replace(',', ' '),
                "nibh molestie mauris, vitae rhoncus dolor lorem vitae dolor. Nam sagittis neque et ligula facilisis condimentum. Sed eros enim".Replace('.', ' ').Replace(',', ' '),
                "consequat in risus a, tempus viverra libero. Donec cursus in nibh et consequat. Ut ex neque, auctor a aliquam a, interdum a ex".Replace('.', ' ').Replace(',', ' '),
                "Nulla mollis rhoncus dui sed luctus. Nunc orci erat, hendrerit in odio nec, facilisis ultrices libero. Donec pretium dui mauris".Replace('.', ' ').Replace(',', ' '),
                "a pulvinar leo porta rhoncus. Aliquam vel erat in leo laoreet commodo. Suspendisse pulvinar viverra tempus. Nunc luctus vitae erat".Replace('.', ' ').Replace(',', ' '),
                "varius egestas. Nullam eu cursus nunc. Nam in velit in nisl ultricies commodo. Aenean consequat facilisis velit, vel molestie lorem".Replace('.', ' ').Replace(',', ' '),
                "sagittis non. Vestibulum lobortis dictum consequat. Aenean eu dignissim elit, sit amet tempor nulla. Integer a pellentesque enim.".Replace('.', ' ').Replace(',', ' '),
                "Cras imperdiet maximus turpis, vel fermentum sapien viverra at. Nunc ornare augue ante, ac sagittis magna auctor eget. Aliquam id ".Replace('.', ' ').Replace(',', ' '),
                "lacus a eros volutpat tincidunt. Fusce lacinia erat at nisi auctor lobortis. Nullam condimentum magna ut sapien convallis posuere.".Replace('.', ' ').Replace(',', ' '),
                "Proin facilisis lobortis mauris, nec efficitur urna. Nunc sit amet libero eget diam tincidunt auctor vel at turpis. Nulla imperdiet".Replace('.', ' ').Replace(',', ' '),
                "urna in lorem consectetur ornare. Morbi iaculis purus eu ultrices ullamcorper. Mauris sollicitudin ac turpis ut sagittis. Pellentesque".Replace('.', ' ').Replace(',', ' '),
                "eleifend augue a urna imperdiet, at hendrerit neque feugiat. Praesent sed enim tellus. Sed malesuada lectus est, non auctor libero congue".Replace('.', ' ').Replace(',', ' '),
                "ut. Aliquam erat volutpat. Morbi faucibus sed nibh at accumsan. Integer a libero libero. Aliquam quis ultrices nisi, sed sagittis ante. ".Replace('.', ' ').Replace(',', ' '),
                "Etiam erat libero, auctor a justo vitae, porta convallis felis. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent mattis".Replace('.', ' ').Replace(',', ' '),
                "laoreet augue, ac mollis massa tempor a. Donec massa ex, interdum non quam ut, aliquet luctus sapien. Aliquam et mauris velit. Cras auctor, nunc".Replace('.', ' ').Replace(',', ' '),
                "et aliquet tincidunt, nibh metus fermentum nisl, ac accumsan leo nisl eget ante. Phasellus ornare metus a lorem aliquet facilisis. Proin risus".Replace('.', ' ').Replace(',', ' '),
                "eros, vehicula id diam nec, luctus commodo diam. Morbi fermentum felis id orci consequat pretium. Duis vulputate lorem metus, vestibulum".Replace('.', ' ').Replace(',', ' '),
                "lobortis velit ullamcorper non. Ut sapien libero, laoreet et varius ac, fringilla et justo. Sed euismod vel velit non molestie. Quisque".Replace('.', ' ').Replace(',', ' '),
                "vitae tellus in urna vehicula posuere quis eget velit. Mauris eu ligula a libero euismod bibendum eget at magna. Vestibulum pretium".Replace('.', ' ').Replace(',', ' '),
                "facilisis eros, eu ornare massa aliquam placerat. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.".Replace('.', ' ').Replace(',', ' '),
                "Nulla facilisi. Mauris nisi ante, imperdiet eget cursus in, suscipit vitae tellus. Maecenas efficitur viverra massa, nec feugiat massa".Replace('.', ' ').Replace(',', ' '),
                "tempus eget. Donec nec velit in sem pulvinar egestas. Donec blandit rhoncus lacus id laoreet. Nunc bibendum lectus ut odio vehicula tempus.".Replace('.', ' ').Replace(',', ' '),
                "In hac habitasse platea dictumst. Vestibulum porttitor dui eu augue ultrices, ut semper nunc consectetur. Aenean in magna hendrerit, porta".Replace('.', ' ').Replace(',', ' '),
                "velit quis, efficitur ante. Quisque at leo dignissim, condimentum arcu in, lacinia justo. Nunc faucibus lobortis diam vitae pellentesque. ".Replace('.', ' ').Replace(',', ' '),
                "Vestibulum a commodo purus, sed porta nulla. Praesent consectetur purus sit amet orci mattis aliquam. Mauris quis luctus mi, eget dignissim".Replace('.', ' ').Replace(',', ' '),
                "metus. Sed sit amet odio iaculis, facilisis dui at, tempus purus. Donec eleifend elementum vulputate. Proin ultricies sagittis lacus, convallis".Replace('.', ' ').Replace(',', ' '),
                "molestie nunc ultrices eu. Integer metus ante, feugiat in nibh sit amet, eleifend faucibus ligula. Curabitur pulvinar pharetra ultricies.".Replace('.', ' ').Replace(',', ' '),
                "Donec ut lacus at ligula finibus feugiat porttitor eget metus. Donec blandit odio non nibh interdum, quis lobortis lacus sollicitudin.".Replace('.', ' ').Replace(',', ' '),
                "Etiam laoreet purus eget nisl accumsan, in aliquet diam porta. Nam libero nibh, luctus at ex nec, dapibus aliquam metus. Pellentesque ".Replace('.', ' ').Replace(',', ' '),
                "porttitor fermentum enim, tincidunt fermentum elit consectetur nec. Nulla lacinia condimentum faucibus. Sed ac mi massa. In hac habitasse".Replace('.', ' ').Replace(',', ' '),
                "platea dictumst. Aenean mattis commodo iaculis. Curabitur rutrum sagittis odio, sed tempus erat tempus eget. Fusce quis convallis enim. Cras".Replace('.', ' ').Replace(',', ' '),
                " eu sagittis urna. Ut eu consectetur massa, at luctus mi. ".Replace('.', ' ').Replace(',', ' ') };
            var dict = new HashSet<string>(linesToCorrect.Aggregate((first, next) => $"{first} {next}").Split(' '));
            dict.Add("intege");
            dict.Add("integr");
            dict.Add("integera");
            var checker = new Checker(dict);
            var result = linesToCorrect.Select(line => checker.CheckLine(line.ToLower(), new char[] { ' ' })).Aggregate((first, next) => $"{first} {next}").Trim();
            var expected = @"lorem ipsum dolor sit amet consectetur adipiscing elit nulla leo enim dignissim et rutru eget cursus consectetur ipsum {suspendisse?} posuere eleifend sapien quis ultrices  {donec?} sed nibh rhoncus  sollicitudin sapien sed scelerisque justo  {donec?} nec erat sed elit vestibulum pharetra  {intege integr integera} ut sagittis urna  vitae lobortis eros  sed id mi non enim maximus faucibus  {proin?} commodo hendrerit rhoncus  {vivamus?} congue nisi at ex venenatis lacinia  in hac habitasse platea dictumst in varius sollicitudin dui  sit amet volutpat nunc volutpat vel  mauris posuere pellentesque urna  volutpat aliquet urna  {morbi?} lorem lorem  elementum nec porttitor non  iaculis id augue  {intege integr integera} mattis  eros eu elementum interdum  turpis est volutpat lectus vel semper metus dui a enim  {donec?} non elementum leo  {morbi?} hendrerit egestas turpis  non tristique metus porta eu  {aenean?} luctus sagittis vestibulum  {quisque?} odio leo  varius eget finibus ut  laoreet ut neque  ut ultricies  massa id lobortis consequat  massa nibh molestie mauris  vitae rhoncus dolor lorem vitae dolor  {nam?} sagittis neque et ligula facilisis condimentum  sed eros enim consequat in risus a  tempus viverra libero  {donec?} cursus in nibh et consequat  ut ex neque  auctor a aliquam a  interdum a ex nulla mollis rhoncus dui sed luctus  nunc orci erat  hendrerit in odio nec  facilisis ultrices libero  {donec?} pretium dui mauris a pulvinar leo porta rhoncus  aliquam vel erat in leo laoreet commodo  {suspendisse?} pulvinar viverra tempus  nunc luctus vitae erat varius egestas  nulla eu cursus nunc  {nam?} in velit in nisl ultricies commodo  {aenean?} consequat facilisis velit  vel molestie lorem sagittis non  vestibulum lobortis dictum consequat  {aenean?} eu dignissim elit  sit amet tempor nulla  {intege integr integera} a pellentesque enim  {cras?} imperdiet maximus turpis  vel fermentum sapien viverra at  nunc ornare augue ante  ac sagittis magna auctor eget  aliquam id  lacus a eros volutpat tincidunt  {fusce?} lacinia erat at nisi auctor lobortis  nulla condimentum magna ut sapien convallis posuere  {proin?} facilisis lobortis mauris  nec efficitur urna  nunc sit amet libero eget diam tincidunt auctor vel at turpis  nulla imperdiet urna in lorem consectetur ornare  {morbi?} iaculis purus eu ultrices ullamcorper  mauris sollicitudin ac turpis ut sagittis  pellentesque eleifend augue a urna imperdiet  at hendrerit neque feugiat  {praesent?} sed enim tellus  sed malesuada lectus est  non auctor libero congue ut  aliquam erat volutpat  {morbi?} faucibus sed nibh at accumsan  {intege integr integera} a libero libero  aliquam quis ultrices nisi  sed sagittis ante   {etiam?} erat libero  auctor a justo vitae  porta convallis felis  interdum et malesuada fames ac ante ipsum primis in faucibus  {praesent?} mattis laoreet augue  ac mollis massa tempor a  {donec?} massa ex  interdum non quam ut  aliquet luctus sapien  aliquam et mauris velit  {cras?} auctor  nunc et aliquet tincidunt  nibh metus fermentum nisl  ac accumsan leo nisl eget ante  {phasellus?} ornare metus a lorem aliquet facilisis  {proin?} risus eros  vehicula id diam nec  luctus commodo diam  {morbi?} fermentum felis id orci consequat pretium  {dui dis} vulputate lorem metus  vestibulum lobortis velit ullamcorper non  ut sapien libero  laoreet et varius ac  fringilla et justo  sed euismod vel velit non molestie  {quisque?} vitae tellus in urna vehicula posuere quis eget velit  mauris eu ligula a libero euismod bibendum eget at magna  vestibulum pretium facilisis eros  eu ornare massa aliquam placerat  orci varius natoque penatibus et magnis dis parturient montes  nascetur ridiculus mus  nulla facilisi  mauris nisi ante  imperdiet eget cursus in  suscipit vitae tellus  {maecenas?} efficitur viverra massa  nec feugiat massa tempus eget  {donec?} nec velit in sem pulvinar egestas  {donec?} blandit rhoncus lacus id laoreet  nunc bibendum lectus ut odio vehicula tempus  in hac habitasse platea dictumst  vestibulum porttitor dui eu augue ultrices  ut semper nunc consectetur  {aenean?} in magna hendrerit  porta velit quis  efficitur ante  {quisque?} at leo dignissim  condimentum arcu in  lacinia justo  nunc faucibus lobortis diam vitae pellentesque   vestibulum a commodo purus  sed porta nulla  {praesent?} consectetur purus sit amet orci mattis aliquam  mauris quis luctus mi  eget dignissim metus  sed sit amet odio iaculis  facilisis dui at  tempus purus  {donec?} eleifend elementum vulputate  {proin?} ultricies sagittis lacus  convallis molestie nunc ultrices eu  {intege integr integera} metus ante  feugiat in nibh sit amet  eleifend faucibus ligula  {curabitur?} pulvinar pharetra ultricies  {donec?} ut lacus at ligula finibus feugiat porttitor eget metus  {donec?} blandit odio non nibh interdum  quis lobortis lacus sollicitudin  {etiam?} laoreet purus eget nisl accumsan  in aliquet diam porta  {nam?} libero nibh  luctus at ex nec  dapibus aliquam metus  pellentesque  porttitor fermentum enim  tincidunt fermentum elit consectetur nec  nulla lacinia condimentum faucibus  sed ac mi massa  in hac habitasse platea dictumst  {aenean?} mattis commodo iaculis  {curabitur?} rutrum sagittis odio  sed tempus erat tempus eget  {fusce?} quis convallis enim  {cras?}  eu sagittis urna  ut eu consectetur massa  at luctus mi";
            Assert.Equal(expected, result);
        }
    }
}
